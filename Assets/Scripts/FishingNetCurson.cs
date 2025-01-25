using UnityEngine;

public class FishingNetCursor : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 0f; 
        transform.position = mousePosition;
    }
}
