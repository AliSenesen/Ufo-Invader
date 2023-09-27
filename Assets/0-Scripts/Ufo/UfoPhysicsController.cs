using System;
using _0_Scripts.Collectables;
using _0_Scripts.Enums;
using _0_Scripts.Events;
using _0_Scripts.GameManager;
using _0_Scripts.Particle;
using DG.Tweening;
using UnityEngine;

namespace Ufo
{
    public class UfoPhysicsController : MonoBehaviour
    {
        [SerializeField] private UfoShieldBarController shieldBarController;
        [SerializeField] private ParticleManager particleManager;


        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IPullable ipullable))
            {
                GameManager.instance.questManager.CollectQuest(ipullable.questItemType);
                particleManager.PlayParticle(ParticleTypes.Collect, transform);
                AudioManager.instance.OnStopSound(AudioStates.Pull);
                AudioManager.instance.OnStopSound(AudioStates.CarPull);
                AudioManager.instance.OnPlaySound(AudioStates.Collect,false);
            }

            if (other.CompareTag("Bullet"))
            {
                Destroy(other.gameObject);
                shieldBarController.DecreaseShield(20);
                particleManager.PlayParticle(ParticleTypes.Damage, transform);
                AudioManager.instance.OnPlaySound(AudioStates.Hit,false);
            }

            if (other.CompareTag("Human"))
            {
                Destroy(other.gameObject);
            }

            if (other.CompareTag("Car"))
            {
                Destroy(other.gameObject);
                shieldBarController.IncreaseShield(20);
            }

            if (other.CompareTag("Animal"))
            {
                Destroy(other.gameObject);
            }

            if (other.TryGetComponent(out CoinController coin))
            {
                coin.DOKill();
                GameEvents.onCoinAdded.Invoke();
                particleManager.PlayParticle(ParticleTypes.Collect, transform);
                AudioManager.instance.OnPlaySound(AudioStates.Coin,false);
                Destroy(other.gameObject);
            }
        }
    }
}