using System.Collections;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Collider2D playerCollider;
    private bool isInvincible = false;
    public float invincibilityDuration = 2f;
    public float blinkInterval = 0.2f;

    void Start()
    {
        spriteRenderer = transform.Find("walk_side").GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Obstacle") && !isInvincible)
        {
            StartCoroutine(ActivateInvincibility());
        }
    }

    IEnumerator ActivateInvincibility()
    {
        isInvincible = true;
        Physics2D.IgnoreLayerCollision(6, 7, true); // 6 = Player, 7 = Obstacle

        float elapsedTime = 0f;
        while (elapsedTime < invincibilityDuration)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(blinkInterval);
            elapsedTime += blinkInterval;
        }

        spriteRenderer.enabled = true;
        isInvincible = false;
        Physics2D.IgnoreLayerCollision(6, 7, false);
    }
}
