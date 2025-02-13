using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    const string playerString = "Player";

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerString))
        {
            OnPickup(); 
            Debug.Log("Pickup Collected");
            Destroy(gameObject);
        }
    }

    protected abstract void OnPickup();
    
}
