using UnityEngine;

namespace Ufo
{
    public class UfoMagnetController : MonoBehaviour
    {
        public static UfoMagnetController instance;
        public int PullForce = 500;


        private void Awake()
        {
            instance = this;
        }


        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out Rigidbody otherRigidbody))

                if (otherRigidbody != null)
                {
                    Vector3 direction = transform.position - other.transform.position;
                    float distance = direction.magnitude;
                    float pullForce = (PullForce * otherRigidbody.mass) / Mathf.Pow(distance, 2);
                    otherRigidbody.AddForce(direction.normalized * pullForce);
                }
        }
    }
}