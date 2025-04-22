using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    public static event Action<int> OnHealthChanged;
    [SerializeField] int health = 3;
    [SerializeField] GameObject player;
    [SerializeField] LevelGenerator levelGenerator;
    [SerializeField] BackgroundScroller backgroundScroller;
    [SerializeField] PlayerEffects playerEffects; // inspector





    private void OnCollisionEnter2D(Collision2D other) {

        if(other.gameObject.CompareTag("Obstacle"))
        {
            
            health--;
            Debug.Log("Health: " + health);

            OnHealthChanged?.Invoke(health);
            /* levelGenerator.StopSpeedBoost();
            backgroundScroller.StopSpeedBoost(); */
            //playerEffects.StopSpeedBoostEffect();

            

            if(health <= 0)
            {
                Debug.Log("PlayerHealth: Game Over");
                // Oyuna ait diğer işlemleri devre dışı bırakın
                levelGenerator.enabled = false;
                backgroundScroller.enabled = false;
                player.SetActive(false);

                // Oyun bitti bilgisini GameManager üzerinden duyurun
                GameManager.GameOver();
            }
        }
    }
}
