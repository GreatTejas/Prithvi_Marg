using UnityEngine;

public class TrashMovement : MonoBehaviour
{
    public float driftSpeed = 0.5f; 
    public float floatStrength = 0.3f; 
    public float rotationSpeed = 5f; 
    public float destroyTime = 15f; 
    public bool randomDriftDirection = true; 

    private Vector3 startPos;
    private float randomOffset;
    private float horizontalDirection; 

    void Start()
    {
        startPos = transform.position;
        randomOffset = Random.Range(0f, Mathf.PI * 2); 
        Destroy(gameObject, destroyTime); 

        horizontalDirection = randomDriftDirection ? Random.Range(-1f, 1f) : 1f;
    }

    void Update()
    {
        transform.position += Vector3.right * driftSpeed * horizontalDirection * Time.deltaTime;

        
        float newY = startPos.y + Mathf.Sin(Time.time + randomOffset) * floatStrength;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        // Rotate slightly to simulate floating rotation
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
