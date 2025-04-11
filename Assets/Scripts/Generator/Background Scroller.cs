using System.Collections;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{

    [SerializeField] float scrollSpeed = 0.5f;
    Material myMaterial;
    Vector2 offSet;

    private bool gameStarted = false;
    private float desiredScrollSpeed;

    void Start()
    {
        myMaterial = GetComponent<Renderer>().material;
        // Asıl kayma hızını saklıyoruz
        desiredScrollSpeed = scrollSpeed;
        // Oyun başlamadan önce arka planın hareket etmemesi için hız 0
        scrollSpeed = 0f;

        offSet = new Vector2(scrollSpeed, 0f);
    }

    void Update()
    {
        if (!gameStarted && Input.GetKeyDown(KeyCode.Space))
        {
            gameStarted = true;
            scrollSpeed = desiredScrollSpeed; // Asıl kayma hızını geri veriyoruz.
            offSet = new Vector2(scrollSpeed, 0f);
            Debug.Log("Background scrolling started at speed: " + scrollSpeed);
        }

        myMaterial.mainTextureOffset += offSet * Time.deltaTime;
        
    }
    public void SpeedUpBackground(float speedAmount, float duration)
    {
        StopAllCoroutines();
        StartCoroutine(SpeedBoostCoroutine(speedAmount, duration));
    }
    
    IEnumerator SpeedBoostCoroutine(float speedAmount, float duration)
    {
        float newSpeed = scrollSpeed + (speedAmount/50);
        offSet = new Vector2(newSpeed, 0f);
        Debug.Log("Speed Boost:" + newSpeed);
        yield return new WaitForSeconds(duration);
        offSet = new Vector2(scrollSpeed, 0f);

    }

}
