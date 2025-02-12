using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    const string playerString = "Player";

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerString))
        {
            OnPickup();
            Destroy(gameObject);
        }
    }

    protected abstract void OnPickup();
    
}
