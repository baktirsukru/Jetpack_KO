using UnityEngine;
using System.Collections;

public class PlayerEffects : MonoBehaviour
{
    [SerializeField] private ParticleSystem speedBoostEffect;
    private Coroutine stopEffectCoroutine;

    public void PlaySpeedBoostEffect(float duration)
    {
        if (speedBoostEffect == null) return;

        speedBoostEffect.Play();

        if (stopEffectCoroutine != null)
            StopCoroutine(stopEffectCoroutine);

        stopEffectCoroutine = StartCoroutine(StopAfter(duration));
    }

    private IEnumerator StopAfter(float duration)
    {
        yield return new WaitForSeconds(duration);
        speedBoostEffect.Stop();
        stopEffectCoroutine = null;
    }

    public void StopSpeedEffect()
    {
        if (stopEffectCoroutine != null)
        {
            StopCoroutine(stopEffectCoroutine);
            stopEffectCoroutine = null;
        }

        if (speedBoostEffect != null && speedBoostEffect.isPlaying)
        {
            speedBoostEffect.Stop();
        }
    }
}
