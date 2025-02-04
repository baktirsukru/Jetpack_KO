using UnityEngine;
using UnityEngine.InputSystem; // Yeni Input System

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] InputAction fly; // Yeni Input System
    [SerializeField] float flyPower = 5f;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        fly.Enable(); // Yeni Input System
    }

    private void FixedUpdate() 
    {
        ProcessFly();
    }

    private void ProcessFly()
    {
        if(fly.IsPressed()) // Yeni Input System
        {
            //Debug.Log("Fly");
            StartFlying();
        }
        else
        StopFlying();
    }

    private void StartFlying()
    {
        Debug.Log("Fly");
        rb.AddRelativeForce(Vector2.up * flyPower * Time.deltaTime);
        //random comment
    }

    private void StopFlying()
    {
        //Debug.Log("Stop Fly");
    }

}
