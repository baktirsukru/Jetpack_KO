using System.Collections;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{

    [Header("Scroll Settings")]
    [SerializeField] private float scrollSpeed = 0.5f;

    private Material myMaterial;
    private Vector2 offset;
    private bool gameStarted = false;
    private float desiredScrollSpeed;
    private Coroutine speedBoostCoroutine;



    void Start()
    {
        myMaterial = GetComponent<Renderer>().material;
        // Store the intended scroll speed and pause until game start
        desiredScrollSpeed = scrollSpeed;
        scrollSpeed = 0f;
        offset = new Vector2(scrollSpeed, 0f);
    }

    void Update()
    {
        if (!gameStarted && Input.GetKeyDown(KeyCode.Space))
        {
            gameStarted = true;
            scrollSpeed = desiredScrollSpeed;
            offset = new Vector2(scrollSpeed, 0f);
            Debug.Log($"Background scrolling started at speed: {scrollSpeed}");
        }

        // Apply texture movement
        myMaterial.mainTextureOffset += offset * Time.deltaTime;
        
    }
    public void SpeedUpBackground(float speedAmount, float duration)
    {
        // Cancel any existing boost
        if (speedBoostCoroutine != null)
            StopCoroutine(speedBoostCoroutine);

        speedBoostCoroutine = StartCoroutine(SpeedBoostCoroutine(speedAmount, duration));

    }
    
    IEnumerator SpeedBoostCoroutine(float speedAmount, float duration)
    {
        float boostedSpeed = scrollSpeed + (speedAmount / 50f);
        offset = new Vector2(boostedSpeed, 0f);
        Debug.Log($"Background speed boosted to: {boostedSpeed}");

        yield return new WaitForSeconds(duration);

        // Reset to normal speed
        offset = new Vector2(scrollSpeed, 0f);
        speedBoostCoroutine = null;

    }

    public void StopSpeedBoostEffect()
    {
        if (speedBoostCoroutine != null)
        {
            StopCoroutine(speedBoostCoroutine);
            speedBoostCoroutine = null;
        }

        offset = new Vector2(scrollSpeed, 0f);
        Debug.Log($"Background boost stopped, speed reset to: {scrollSpeed}");
    }




}
