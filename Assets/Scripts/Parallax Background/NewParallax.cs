using UnityEngine;

public class NewParallax : MonoBehaviour
{
    private float length, startPos;
    public GameObject platform;
    public float parallaxEffect;

    void Start()
    {
        startPos = transform.position.x;
        length = platform.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {
        float temp = (platform.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startPos + temp, transform.position.y, transform.position.z);
    }
}
