using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour
{
    [Header("Invincibility Settings")]
    [SerializeField] private float invincibilityDuration = 2f;
    [SerializeField] private float blinkInterval = 0.2f;
    [SerializeField] private int playerLayer = 9;
    [SerializeField] private int obstacleLayer = 8;

    private SpriteRenderer spriteRenderer;
    private Collider2D playerCollider;
    private PlayerEffects playerEffects;
    private bool isInvincible = false;

    void Awake()
    {
        // Cache references
        spriteRenderer = transform.Find("Astro").GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<Collider2D>();
        playerEffects = GetComponent<PlayerEffects>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Check for obstacle hit and invincibility
        if (!isInvincible && other.gameObject.layer == obstacleLayer)
        {
            // Immediately stop any active speed boost effect
            playerEffects?.StopSpeedBoostEffect();

            // Reset score multiplier
            ScoreManager.Instance.ResetMultiplier();

            // Begin temporary invincibility
            StartCoroutine(ActivateInvincibility());
        }
    }

    private IEnumerator ActivateInvincibility()
    {
        isInvincible = true;

        // Ignore collisions with obstacles
        Physics2D.IgnoreLayerCollision(playerLayer, obstacleLayer, true);

        float elapsed = 0f;
        while (elapsed < invincibilityDuration)
        {
            // Blink effect
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(blinkInterval);
            elapsed += blinkInterval;
        }

        // Restore visibility and collision
        spriteRenderer.enabled = true;
        Physics2D.IgnoreLayerCollision(playerLayer, obstacleLayer, false);
        isInvincible = false;
    }
}
