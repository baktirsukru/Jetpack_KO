using UnityEngine;
using UnityEngine.InputSystem; // Yeni Input System

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] InputAction Fly; // Yeni Input System
    [SerializeField] float flyPower = 5f;
    [SerializeField] Animator animator;
    Rigidbody2D rb;

    public bool isRunning = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        Fly.Enable(); // Yeni Input System
    }
    private void OnDisable()
    {
        Fly.Disable(); // Yeni Input System
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

        // Animasyon geçişi: Player_idle -> Player_Fly
        animator.SetBool("isFlying", true); 
        
    }

    private void StopFlying()
    {
        //Debug.Log("Stop Fly");

        // Animasyon geçişi: Player_idle -> Player_Fly
        animator.SetBool("isFlying", false);
    }

    private void CheckRunning()
    {
        animator.SetBool("isRunning", isRunning);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isRunning = true;
            CheckRunning();
            
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isRunning = false;
            CheckRunning();
            
        }
    }

    
}