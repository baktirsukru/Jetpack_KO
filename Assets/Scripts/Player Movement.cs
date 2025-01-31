using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float thrustForce = 5f;
    [SerializeField] float maxYSpeed = 5f;
    
    Rigidbody2D rb;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            //rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Clamp(rb.linearVelocity.y + thrustForce * Time.deltaTime, -maxYSpeed, maxYSpeed));
            Debug.Log("Space key was pressed");
        }

        //denemelik comment
    }
}
