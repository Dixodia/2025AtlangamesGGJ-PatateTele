using UnityEngine;

public class ParticleEffectManager : MonoBehaviour
{
    // This method plays the particle effect at the given transform
    public static ParticleEffectManager instance;

    public GameObject[] particleList;

    private void Awake()
    {
        instance = this;
    }

    public void PlayParticleEffect(Transform targetTransform, GameObject particleEffectPrefab)
    {
        if (targetTransform == null || particleEffectPrefab == null)
        {
            Debug.LogError("Target Transform or Particle Effect Prefab is null!");
            return;
        }

        // Instantiate the particle effect at the target transform's position, rotation, and scale
        GameObject particleEffect = Instantiate(particleEffectPrefab, targetTransform.position, targetTransform.rotation);

        // Optionally, if you want the particle effect to scale with the target transform:
        particleEffect.transform.localScale = targetTransform.localScale;

        // Optionally, if you want the particle effect to play and destroy automatically after it's finished
        ParticleSystem particleSystem = particleEffect.GetComponent<ParticleSystem>();
        if (particleSystem != null)
        {
            particleSystem.Play();
            foreach (ParticleSystem particle in particleEffect.transform.GetComponentsInChildren<ParticleSystem>())
            {
                particle.Play();
            }
            // Destroy the particle effect object after it finishes playing
            Destroy(particleEffect, particleSystem.main.duration);
        }
    }
}