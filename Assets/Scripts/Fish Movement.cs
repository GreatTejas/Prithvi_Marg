using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public RectTransform fishTransform; 
    public float speed = 100f;
    public float verticalRange = 50f; 
    public RectTransform canvasRect;

    private Vector2 startPos;
    private float randomOffset;
    private float screenWidth;

    void Start()
    {
        screenWidth = canvasRect.rect.width;

        startPos = fishTransform.anchoredPosition;
        randomOffset = Random.Range(0f, Mathf.PI * 2);
        if (speed > 0)
            fishTransform.anchoredPosition = new Vector2(-screenWidth / 2, startPos.y); 
        else
            fishTransform.anchoredPosition = new Vector2(screenWidth / 2, startPos.y); 
    }

    void Update()
    {
        
        fishTransform.anchoredPosition += Vector2.right * speed * Time.deltaTime;
        float verticalMovement = Mathf.Sin(Time.time + randomOffset) * verticalRange;
        fishTransform.anchoredPosition = new Vector2(fishTransform.anchoredPosition.x, startPos.y + verticalMovement);
        if (Mathf.Abs(fishTransform.anchoredPosition.x) > screenWidth / 2)
        {
            ResetFish();
        }
    }

    void ResetFish()
    {
        float newY = startPos.y + Random.Range(-verticalRange, verticalRange);
        if (speed > 0) 
            fishTransform.anchoredPosition = new Vector2(-screenWidth / 2, newY);
        else 
            fishTransform.anchoredPosition = new Vector2(screenWidth / 2, newY);
    }
}
