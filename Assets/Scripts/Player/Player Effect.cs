using UnityEngine;
using System.Collections;

public class PlayerEffects : MonoBehaviour
{
    [SerializeField] private ParticleSystem speedBoostEffect;

    public void PlaySpeedBoostEffect(float duration)
    {
        if (speedBoostEffect == null) return;

        speedBoostEffect.Play();
        StartCoroutine(StopAfter(duration));
    }

    private IEnumerator StopAfter(float duration)
    {
        yield return new WaitForSeconds(duration);
        speedBoostEffect.Stop();
    }
}
