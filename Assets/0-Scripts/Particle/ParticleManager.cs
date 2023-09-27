using _0_Scripts.Enums;
using UnityEngine;

namespace _0_Scripts.Particle
{
    public class ParticleManager : MonoBehaviour
    {
        [SerializeField] private ParticleSystem damageParticle;
        [SerializeField] private ParticleSystem collectParticle;
        [SerializeField] private ParticleSystem upgradeParticle;

        private ParticleTypes _particleTypes;

        public void PlayParticle(ParticleTypes particleType, Transform targetTransform)
        {
            ParticleSystem selectedParticlePrefab = null;

            switch (particleType)
            {
                case ParticleTypes.Damage:
                    selectedParticlePrefab = damageParticle;
                    break;
                case ParticleTypes.Collect:
                    selectedParticlePrefab = collectParticle;
                    break;
                case ParticleTypes.Upgrade:
                    selectedParticlePrefab = upgradeParticle;
                    break;
            }

            if (selectedParticlePrefab != null)
            {
                ParticleSystem particle = Instantiate(selectedParticlePrefab, targetTransform.position, Quaternion.identity);
                particle.Play();
                Destroy(particle.gameObject, .5f);
            }
        }
    }
}