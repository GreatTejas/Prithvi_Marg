using UnityEngine;

public class FishMovementUIOpposite : MonoBehaviour
{
    public RectTransform fishTransform;
    public float speed = -100f; 
    public float verticalRange = 50f; 
    public RectTransform canvasRect; 

    private Vector2 startPos;
    private float randomOffset;
    private float screenWidth;
    private float screenHeight;

    void Start()
    {
        screenWidth = canvasRect.rect.width;
        screenHeight = canvasRect.rect.height;

        startPos = fishTransform.anchoredPosition;
        randomOffset = Random.Range(0f, Mathf.PI * 2); 
        fishTransform.anchoredPosition = new Vector2(screenWidth / 2, Random.Range(-verticalRange, verticalRange));
    }

    void Update()
    {
        fishTransform.anchoredPosition += Vector2.left * Mathf.Abs(speed) * Time.deltaTime;
        float verticalMovement = Mathf.Sin(Time.time + randomOffset) * verticalRange;
        fishTransform.anchoredPosition = new Vector2(fishTransform.anchoredPosition.x, startPos.y + verticalMovement);
        if (fishTransform.anchoredPosition.x < -screenWidth / 2)
        {
            ResetFish();
        }
    }

    void ResetFish()
    {
        float newY = Random.Range(-verticalRange, verticalRange); 
        fishTransform.anchoredPosition = new Vector2(screenWidth / 2, newY);
    }
}
