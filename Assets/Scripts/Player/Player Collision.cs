using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerCollision : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Collider2D playerCollider;
    private bool isInvincible = false;
    public float invincibilityDuration = 2f;
    public float blinkInterval = 0.2f;

    void Start()
    {
        spriteRenderer = transform.Find("Astro").GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Obstacle") && !isInvincible)
        {
            ScoreManager.Instance.ResetMultiplier();
            StartCoroutine(ActivateInvincibility());
        }
    }

    IEnumerator ActivateInvincibility()
    {
        isInvincible = true;
        //engele çarpmasını engelle
        Physics2D.IgnoreLayerCollision(9, 8, true); // 6 = Player, 7 = Obstacle  9 = PlayerLayer, 8 = ObstacleLayer
        // Blink efekti

        float elapsedTime = 0f;
        while (elapsedTime < invincibilityDuration)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(blinkInterval);
            elapsedTime += blinkInterval;
        }

        spriteRenderer.enabled = true;
        isInvincible = false;
        //engele çarpmasını tekrar aktive et
        Physics2D.IgnoreLayerCollision(9, 8, false);
    }
}
