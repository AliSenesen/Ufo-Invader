using System;
using DG.Tweening;
using Ufo;
using UnityEngine;

namespace _0_Scripts.Collectables
{
    public class CoinController : MonoBehaviour
    {
        [SerializeField] private float rotationDuration = 2f;

        private void Start()
        {
            CoinRotate();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out UfoMagnetController ufo))
            {
                transform.DOMove(ufo.transform.position, .5f);
            }
        }


        private void CoinRotate()
        {
            transform.DORotate
                    (new Vector3(0f, 360f, 0f), rotationDuration, RotateMode.WorldAxisAdd)
                .SetLoops(-1, LoopType.Incremental);
        }
    }
}