using System;
using System.Collections;
using System.Collections.Generic;
using _0_Scripts.Collectables;
using _0_Scripts.Events;
using DG.Tweening;
using Enums;
using Ufo;
using UnityEngine;

public class CarController : IPullable
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject leftBorder;
    [SerializeField] private GameObject rightBorder;
    [SerializeField] private GameObject bullyImage;
    [SerializeField] private GameObject scaredImage;
    [SerializeField] private Transform cameraTransform;


    private float _speed = 4.5f;
    private bool _isGrounded = false;
    private bool _canMove = false;

    private void OnEnable()
    {
        GameEvents.onRadarClosed.AddListener(OnRadarClosed);
    }

    private void OnDisable()
    {
        GameEvents.onRadarClosed.RemoveListener(OnRadarClosed);
    }


    private void Start()
    {
        _isGrounded = true;
        _canMove = true;
        CloseEmojis();
    }

    private void Update()
    {
        if (transform.position.y <= 3 && rb.useGravity)
        {
            Grounded();
        }
    }

    private void FixedUpdate()
    {
        if (transform.position.y <= 3 && _isGrounded)
        {
            Move();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LeftBorder"))
        {
            GoRightSide();
        }

        if (other.CompareTag("RightBorder"))
        {
            GoLeftSide();
        }

        if (other.TryGetComponent(out UfoMagnetController ufo))
        {
            _isGrounded = false;
            _canMove = false;
            isPaniced = true;
            AudioManager.instance.OnPlaySound(AudioStates.CarPull,false);
        }

      
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out UfoMagnetController ufo))
        {
            Pulled();
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out UfoMagnetController ufo))
        {
            BreakBound();
            AudioManager.instance.OnStopSound(AudioStates.CarPull);
        }
    }

    private void Move()
    {
        if (_canMove && _isGrounded)
        {
            if (!isPaniced)
            {
                rb.velocity = transform.forward * _speed;
            }

            if (isPaniced)
            {
                rb.velocity = transform.forward * (_speed * 2.5f);
            }
        }
    }

    private void GoRightSide()
    {
        transform.position =
            new Vector3(rightBorder.transform.position.x - 3, transform.position.y, transform.position.z);
    }

    private void GoLeftSide()
    {
        transform.position =
            new Vector3(leftBorder.transform.position.x + 3, transform.position.y, transform.position.z);
    }


    public override void Pulled()
    {
        rb.velocity = new Vector3(0, rb.velocity.y, 0);
        _canMove = false;
        rb.useGravity = false;
        _isGrounded = false;
        OpenScaredEmoji();
    }

    public override void BreakBound()
    {
        rb.useGravity = true;
        _canMove = false;
        _isGrounded = false;
        OpenBullyEmoji();
    }

    public override void Grounded()
    {
        _canMove = true;
        rb.useGravity = true;
        _isGrounded = true;
        if (!isPaniced)
        {
            CloseEmojis();
        }
    }

    private void OnRadarClosed()
    {
        if (rb.useGravity == false)
        {
            rb.useGravity = true;
        }
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

    public override void Panic()
    {
    }
}