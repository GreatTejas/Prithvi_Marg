using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPosition;
    private float zOffset;
    private bool isDragging = false;
    private BoxCollider collider;

    // Lights for both bins
    private Light greenLightLeft, greenLightRight;
    private Light redLightLeft, redLightRight;
    // Sound Effects
    public AudioClip dropSound; 
    private AudioSource audioSource;
      //  public GameController gameController; 

    private void Start()
    {
        collider = GetComponent<BoxCollider>();
       

       

        zOffset = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);

        // Assign lights
        greenLightLeft = GameObject.Find("greenLightLeft")?.GetComponent<Light>();
        greenLightRight = GameObject.Find("greenLightRight")?.GetComponent<Light>();
        redLightLeft = GameObject.Find("redLightLeft")?.GetComponent<Light>();
        redLightRight = GameObject.Find("redLightRight")?.GetComponent<Light>();

        if (!greenLightLeft || !greenLightRight || !redLightLeft || !redLightRight)
        {
            Debug.LogError("Some lights are missing. Please ensure all four lights are assigned.");
        }
        // Set up the AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }
    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Drop sound or AudioSource is not set.");
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (collider == null || !IsCorrectObjectSelected()) return;

        startPosition = transform.position;
        isDragging = true;
        collider.enabled = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging) return;

        Vector3 screenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, zOffset);
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(screenPoint);
        transform.position = worldPoint;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isDragging) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (IsCorrectBin(hit.collider.gameObject))
            {
                Debug.Log($"Correct placement! {gameObject.name} placed in {hit.collider.gameObject.tag}.");
                ActivateBothLights(greenLightLeft, greenLightRight);
                // Notify GameController of a correct placement
                    PlaySound(dropSound);
               FindObjectOfType<Gm>()?.ObjectPlaced();
                          // gameController.ObjectPlacedCorrectly();
                // Use the updated method to find the GameController
    /*GameController gameController = Object.FindFirstObjectByType<GameController>();
    gameController?.ObjectPlacedCorrectly();*/
                StartCoroutine(DestroyAfterDelay(1f));
            }
            else
            {
                Debug.Log($"Incorrect placement! {gameObject.name} placed in {hit.collider.gameObject.name}.");
                ActivateBothLights(redLightLeft, redLightRight);
                transform.position = startPosition;
            }
        }
        else
        {
            transform.position = startPosition;
        }

        collider.enabled = true;
        isDragging = false;
    }

    public bool IsCorrectBin(GameObject bin)
    {
        return (gameObject.tag == "Dry" && bin.tag == "DryBin") || (gameObject.tag == "Wet" && bin.tag == "WetBin");
    }

    private void ActivateBothLights(Light leftLight, Light rightLight)
    {
        ActivateLight(leftLight);
        ActivateLight(rightLight);
    }

    private Coroutine currentCoroutineLeft, currentCoroutineRight;

   private void ActivateLight(Light light)
{
    if (light == null) return;

    if (light == greenLightLeft || light == redLightLeft)
    {
        // Check if the left coroutine exists before stopping it
        if (currentCoroutineLeft != null)
        {
            StopCoroutine(currentCoroutineLeft);
        }
        currentCoroutineLeft = StartCoroutine(FlashLight(light));
    }
    else if (light == greenLightRight || light == redLightRight)
    {
        // Check if the right coroutine exists before stopping it
        if (currentCoroutineRight != null)
        {
            StopCoroutine(currentCoroutineRight);
        }
        currentCoroutineRight = StartCoroutine(FlashLight(light));
    }
}

    private IEnumerator FlashLight(Light light)
    {
        if (light == redLightLeft){
            light.intensity=225;
        }
        if (light == redLightRight){
            light.intensity=103;
        }
         if (light == greenLightRight){
            light.intensity=10.8f;
        }
          if (light == greenLightLeft){
            light.intensity=157.9f;
        }
        yield return new WaitForSeconds(0.5f);
        light.intensity = 0;
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    private bool AdjustColliderToFitObject()
    {
        if (collider == null) return false;

        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            AdjustColliderWithBounds(meshRenderer.bounds);
            return true;
        }

        SkinnedMeshRenderer skinnedRenderer = GetComponent<SkinnedMeshRenderer>();
        if (skinnedRenderer != null)
        {
            AdjustColliderWithBounds(skinnedRenderer.bounds);
            return true;
        }

        return false;
    }

    private void AdjustColliderWithBounds(Bounds bounds)
    {
        collider.center = bounds.center - transform.position;
        collider.size = bounds.size;
    }

    private bool IsCorrectObjectSelected()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out RaycastHit hit) && hit.collider.gameObject == gameObject;
    }
}