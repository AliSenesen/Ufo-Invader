using System;
using _0_Scripts.Collectables;
using _0_Scripts.Events;
using Ufo;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class HumanController : IPullable
{
    [SerializeField] private float wanderRadius;
    [SerializeField] private float wanderTimer;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private HumanAnimationController animationController;
    [SerializeField] private GameObject bullyImage;
    [SerializeField] private GameObject scaredImage;
    [SerializeField] private Transform cameraTransform;


    private float _timer;
    private bool _isAgentActive;
    private bool _isGrounded;

    private void OnEnable()
    {
        GameEvents.onRadarClosed.AddListener(OnRadarClosed);
    }

    private void OnDisable()
    {
        GameEvents.onRadarClosed.RemoveListener(OnRadarClosed);
    }


    void Start()
    {
        bullyImage.SetActive(false);
        scaredImage.SetActive(false);
        _isGrounded = true;
        _isAgentActive = true;
        _timer = wanderTimer;
        animationController.StartWalkAnim();
    }

    void Update()
    {
        ChangeDestination();

        if (!_isGrounded && rb.velocity.magnitude < 0.1f)
        {
            Grounded();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out UfoMagnetController magnetController))
        {
            AudioManager.instance.OnPlaySound(AudioStates.Pull, false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out UfoMagnetController magnetController))
        {
            isPaniced = true;
            Pulled();
            AudioManager.instance.OnPlaySound(AudioStates.Pull, false);
        }

        if (other.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out UfoMagnetController magnetController))
        {
            BreakBound();
            AudioManager.instance.OnStopSound(AudioStates.Pull);
        }
    }


    public override void Pulled()
    {
        agent.enabled = false;
        _isAgentActive = false;
        rb.useGravity = false;
        animationController.StartFallAnim();
        OpenScaredEmoji();
    }


    public override void BreakBound()
    {
        rb.useGravity = true;
        agent.enabled = false;
        _isAgentActive = false;
        _isGrounded = false;
        animationController.StartFallAnim();
        OpenBullyEmoji();
    }

    public override void Grounded()
    {
        rb.useGravity = true;
        agent.enabled = true;
        _isAgentActive = true;
        _isGrounded = true;
        if (!isPaniced)
        {
            animationController.StartWalkAnim();
            CloseEmojis();
        }
        else
        {
            animationController.StartRunAnim();
        }
    }

    private void OnRadarClosed()
    {
        if (rb.useGravity == false)
        {
            rb.useGravity = true;
        }
    }

    public override void Panic()
    {
        if (isPaniced && _isGrounded)
        {
            animationController.StartRunAnim();
            OpenBullyEmoji();
        }
    }

    private void ChangeAgentSpeed(float speedIncrease)
    {
        agent.speed += speedIncrease;
    }

    private void OpenScaredEmoji()
    {
        scaredImage.SetActive(true);
        bullyImage.SetActive(false);
        scaredImage.transform.LookAt(cameraTransform);
        scaredImage.transform.rotation = Quaternion.Euler(0, 130, 0);
    }

    private void OpenBullyEmoji()
    {
        bullyImage.SetActive(true);
        scaredImage.SetActive(false);
        bullyImage.transform.LookAt(cameraTransform);
        bullyImage.transform.rotation = Quaternion.Euler(0, 130, 0);
    }

    private void CloseEmojis()
    {
        bullyImage.SetActive(false);
        scaredImage.SetActive(false);
    }


    private void ChangeDestination()
    {
        _timer += Time.deltaTime;

        if (_timer >= wanderTimer)
        {
            if (_isAgentActive)
            {
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                agent.SetDestination(newPos);
                _timer = 0;
            }
        }

        if (isPaniced && _isGrounded && agent.speed <= 4)
        {
            ChangeAgentSpeed(.4f);
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
}