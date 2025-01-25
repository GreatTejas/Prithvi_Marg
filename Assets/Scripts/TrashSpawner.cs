using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    public GameObject[] trashPrefabs;
    public float spawnInterval = 3f;
    public float maxTrashLifetime = 10f; 
    public float spawnDuration = 45f; 

    public RectTransform canvasRect; 
    private float elapsedTime = 0f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnTrash), 1f, spawnInterval);
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= spawnDuration)
        {
            CancelInvoke(nameof(SpawnTrash));
        }
    }

    void SpawnTrash()
    {
        if (trashPrefabs.Length == 0) return; 
        GameObject trash = trashPrefabs[Random.Range(0, trashPrefabs.Length)];
        float randomX = Random.Range(-canvasRect.rect.width / 2, canvasRect.rect.width / 2);
        float randomY = Random.Range(-canvasRect.rect.height / 2, canvasRect.rect.height / 2);
        Vector3 spawnPos = new Vector3(randomX, randomY, 0f);
        GameObject spawnedTrash = Instantiate(trash, spawnPos, Quaternion.identity);
        spawnedTrash.transform.SetParent(canvasRect, false);

        RectTransform trashRect = spawnedTrash.GetComponent<RectTransform>();
        if (trashRect != null)
        {
            trashRect.localPosition = spawnPos;
        }

        Destroy(spawnedTrash, maxTrashLifetime);
    }
}
