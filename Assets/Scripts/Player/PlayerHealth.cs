using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int health = 3;
    [SerializeField] GameObject player;
    [SerializeField] LevelGenerator levelGenerator;
    [SerializeField] BackgroundScroller backgroundScroller;




    private void OnCollisionEnter2D(Collision2D other) {

        if(other.gameObject.CompareTag("Obstacle"))
        {
            health--;
            Debug.Log("Health: " + health);
            

            if(health <= 0)
            {
                Debug.Log("Game Over");
                levelGenerator.enabled = false;
                backgroundScroller.enabled = false;
                player.SetActive(false);
            }
        }
    }
}
