using UnityEngine;
using UnityEngine.UI;

public class puzzleselector : MonoBehaviour
{
    public GameObject Startpanel;
    public void Setpuzzlesphoto(Image photo)
    {
        Startpanel.SetActive(false);

        for (int i = 0; i < 16; i++)
        {
            GameObject.Find("Piece(" + i + ")").transform.Find("image").GetComponent<SpriteRenderer>().sprite = photo.sprite;
        }
    }
}

