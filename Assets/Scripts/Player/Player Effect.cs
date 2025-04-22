using UnityEngine;
using System.Collections;

public class PlayerEffects : MonoBehaviour
{
    [SerializeField] private ParticleSystem speedBoostEffect;
    private Coroutine stopEffectCoroutine;

    public void PlaySpeedBoostEffect(float duration)
    {
        if (speedBoostEffect == null) return;

        // cancel any pending stop and current emission
        StopSpeedBoostEffect();

        // play the effect anew
        speedBoostEffect.Play();

        // schedule stopping
        stopEffectCoroutine = StartCoroutine(StopAfter(duration));
    }

    private IEnumerator StopAfter(float duration)
    {
        yield return new WaitForSeconds(duration);
        StopSpeedBoostEffect();
        
    }

    public void StopSpeedBoostEffect()
    {
        // cancel the scheduled stop
        if (stopEffectCoroutine != null)
        {
            StopCoroutine(stopEffectCoroutine);
            stopEffectCoroutine = null;
        }

        // if it's playing, stop emitting (particles already alive will fade out)
        if (speedBoostEffect != null && speedBoostEffect.isPlaying)
        {
            speedBoostEffect.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }
}
