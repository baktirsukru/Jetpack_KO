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
}
