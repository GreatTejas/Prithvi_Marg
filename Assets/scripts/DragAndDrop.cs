using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    public GameObject SelectedPiece;
    private Vector3 correctPosition;
    private ScoreManager scoreManager;
    private bool canDrag = true;
    private AudioSource audioSource;
    public AudioClip movePieceSound;
    public AudioClip correctPlacementSound;
    private float moveSoundCooldown = 0.2f;
    private float lastMoveSoundTime;

    private void Start()
    {
        SelectedPiece = null;
        scoreManager = FindObjectOfType<ScoreManager>();
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.volume = 0.2f;
        audioSource.pitch = 1f;
    }

    void Update()
    {
        if (!canDrag) return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform.CompareTag("Puzzle1"))
                {
                    SelectedPiece = hit.transform.gameObject;
                    correctPosition = new Vector3(-8.007912f, 31.86056f, -0.01950645f);
                }
                if (hit.transform.CompareTag("Puzzle2"))
                {
                    SelectedPiece = hit.transform.gameObject;
                    correctPosition = new Vector3(-6.056035f, 32.15602f, -0.01950645f);
                }
                if (hit.transform.CompareTag("Puzzle3"))
                {
                    SelectedPiece = hit.transform.gameObject;
                    correctPosition = new Vector3(-4.129255f, 31.84915f, -0.01950645f);
                }
                if (hit.transform.CompareTag("Puzzle4"))
                {
                    SelectedPiece = hit.transform.gameObject;
                    correctPosition = new Vector3(-2.452306f, 32.14576f, -0.01950645f);
                }
                if (hit.transform.CompareTag("Puzzle5"))
                {
                    SelectedPiece = hit.transform.gameObject;
                    correctPosition = new Vector3(-7.711309f, 30.19502f, -0.01950645f);
                }
                if (hit.transform.CompareTag("Puzzle6"))
                {
                    SelectedPiece = hit.transform.gameObject;
                    correctPosition = new Vector3(-6.057175f, 30.18361f, -0.01950645f);
                }
                if (hit.transform.CompareTag("Puzzle7"))
                {
                    SelectedPiece = hit.transform.gameObject;
                    correctPosition = new Vector3(-4.129255f, 30.20642f, -0.01950645f);
                }
                if (hit.transform.CompareTag("Puzzle8"))
                {
                    SelectedPiece = hit.transform.gameObject;
                    correctPosition = new Vector3(-2.16711f, 30.20642f, -0.01950645f);
                }
                if (hit.transform.CompareTag("Puzzle9"))
                {
                    SelectedPiece = hit.transform.gameObject;
                    correctPosition = new Vector3(-7.99f, 28.23f, -0.01950645f);
                }
                if (hit.transform.CompareTag("Puzzle10"))
                {
                    SelectedPiece = hit.transform.gameObject;
                    correctPosition = new Vector3(-6.057175f, 28.2671f, -0.01950645f);
                }
                if (hit.transform.CompareTag("Puzzle11"))
                {
                    SelectedPiece = hit.transform.gameObject;
                    correctPosition = new Vector3(-4.12f, 28.3f, -0.01950645f);
                }
                if (hit.transform.CompareTag("Puzzle12"))
                {
                    SelectedPiece = hit.transform.gameObject;
                    correctPosition = new Vector3(-2.46f, 28.31f, -0.01950645f);
                }
                if (hit.transform.CompareTag("Puzzle13"))
                {
                    SelectedPiece = hit.transform.gameObject;
                    correctPosition = new Vector3(-7.721576f, 26.31408f, -0.01950645f);
                }
                if (hit.transform.CompareTag("Puzzle14"))
                {
                    SelectedPiece = hit.transform.gameObject;
                    correctPosition = new Vector3(-6.045768f, 26.61296f, -0.01950645f);
                }
                if (hit.transform.CompareTag("Puzzle15"))
                {
                    SelectedPiece = hit.transform.gameObject;
                    correctPosition = new Vector3(-4.106439f, 26.35058f, -0.01950645f);
                }
                if (hit.transform.CompareTag("Puzzle16"))
                {
                    SelectedPiece = hit.transform.gameObject;
                    correctPosition = new Vector3(-2.16711f, 26.60155f, -0.01950645f);
                }
            }
        }

        if (SelectedPiece != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 targetPosition = ray.GetPoint(20f);

            // Controlled move sound to prevent gargling
            if (movePieceSound != null && Time.time - lastMoveSoundTime > moveSoundCooldown)
            {
                audioSource.PlayOneShot(movePieceSound, 0.1f);
                lastMoveSoundTime = Time.time;
            }

            if (Vector3.Distance(SelectedPiece.transform.position, correctPosition) < 0.2f)
            {
                SelectedPiece.transform.position = correctPosition;

                if (correctPlacementSound != null)
                {
                    audioSource.PlayOneShot(correctPlacementSound, 0.3f);
                }

                scoreManager.OnPuzzlePiecePlaced();
                SelectedPiece = null;
            }
            else
            {
                SelectedPiece.transform.position = new Vector3(targetPosition.x, targetPosition.y, SelectedPiece.transform.position.z);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (SelectedPiece != null)
            {
                SelectedPiece = null;
            }
        }
    }

    public void EnableDragging(bool enable)
    {
        canDrag = enable;
    }
}