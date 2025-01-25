using UnityEngine;

public class TrashClickHandler : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))  
        {
            ScoreManager.instance.AddScore(10);

            Destroy(gameObject);
        }
    }
}
