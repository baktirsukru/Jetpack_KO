using UnityEngine;
using UnityEngine.InputSystem; // Yeni Input System

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] InputAction Fly; // Yeni Input System
    [SerializeField] float flyPower = 5f;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        Fly.Enable(); // Yeni Input System
    }

    private void FixedUpdate() 
    {
        ProcessFly();
    }

    private void ProcessFly()
    {
        if(Fly.IsPressed()) // Yeni Input System
        {
            //Debug.Log("Fly");
            StartFlying();
        }
        else
        StopFlying();
    }

    private void StartFlying()
    {
        //Debug.Log("Fly");
        rb.AddRelativeForce(Vector2.up * flyPower * Time.deltaTime);
        
    }

    private void StopFlying()
    {
        //Debug.Log("Stop Fly");
    }
}