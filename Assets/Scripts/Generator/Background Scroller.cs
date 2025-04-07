using System.Collections;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{

    [SerializeField] float scrollSpeed = 0.5f;
    Material myMaterial;
    Vector2 offSet;
    void Start()
    {
        myMaterial = GetComponent<Renderer>().material;
        offSet = new Vector2(scrollSpeed, 0f);
    }


    void Update()
    {
        myMaterial.mainTextureOffset += offSet * Time.deltaTime;
        /* float newX = Mathf.Repeat(myMaterial.mainTextureOffset.x + scrollSpeed * Time.deltaTime, 1f);
        myMaterial.mainTextureOffset = new Vector2(newX, 0f); */
    }
    public void SpeedUpBackground(float speedAmount, float duration)
    {
        StopAllCoroutines();
        StartCoroutine(SpeedBoostCoroutine(speedAmount, duration));
    }
    
    IEnumerator SpeedBoostCoroutine(float speedAmount, float duration)
    {
        float newSpeed = scrollSpeed + (speedAmount/10);
        offSet = new Vector2(newSpeed, 0f);
        Debug.Log("Speed Boost:" + newSpeed);
        yield return new WaitForSeconds(duration);
        offSet = new Vector2(scrollSpeed, 0f);

    }

}
