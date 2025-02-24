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
    }
    public void SpeedUpBackground(float speedAmount, float duration)
    {
        StartCoroutine(SpeedBoostCoroutine(speedAmount, duration));
    }
    
    IEnumerator SpeedBoostCoroutine(float speedAmount, float duration)
    {
        float newSpeed = scrollSpeed + speedAmount;
        offSet = new Vector2(newSpeed, 0f);
        yield return new WaitForSeconds(duration);
        offSet = new Vector2(scrollSpeed, 0f);
    }

}
