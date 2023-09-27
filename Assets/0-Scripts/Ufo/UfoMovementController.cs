using _0_Scripts.Events;
using UnityEngine;

namespace Ufo
{
    public class UfoMovementController : MonoBehaviour
    {
        public float MovementSpeed = 4.5f;
        public static UfoMovementController instance;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private FloatingJoystick joystick;
        [SerializeField] private GameObject radar;

        private void Awake()
        {
            instance = this;
        }

        void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            Vector3 movementDirection = new Vector3(joystick.Horizontal, 0f, joystick.Vertical);
            movementDirection = Quaternion.Euler(0f, -30f, 0f) * movementDirection;

            rb.velocity = movementDirection * MovementSpeed;

            if (joystick.Horizontal != 0 || joystick.Vertical != 0)
            {
                transform.rotation = Quaternion.LookRotation(rb.velocity);
                radar.SetActive(false);
                GameEvents.onRadarClosed.Invoke();
            }
            else
            {
                transform.rotation = Quaternion.identity;
                radar.SetActive(true);
            }
        }
    }
}