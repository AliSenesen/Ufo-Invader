using System;
using System.Collections;
using _0_Scripts.Events;
using GameAnalyticsSDK.Setup;
using Ufo;
using UnityEngine;

public class TurretController : MonoBehaviour
{
   
    [SerializeField] private Transform ufoTransform;
    [SerializeField] private Transform barrel;
    [SerializeField] private GameObject projectile;

    private float _lookSpeed = 3.5f;
    private bool _isEntered;
    private bool _canFire;
    private Coroutine _shootingCoroutine;

    private void OnEnable()
    {
        RegisterEvents();
    }

    private void RegisterEvents()
    {
        GameEvents.onWin.AddListener(CloseShoot);
        GameEvents.onFail.AddListener(CloseShoot);
    }

    private void UnRegisterEvents()
    {
        GameEvents.onWin.RemoveListener(CloseShoot);
        GameEvents.onFail.RemoveListener(CloseShoot);
    }

    private void OnDisable()
    {
        UnRegisterEvents();
    }

    private void Update()
    {
        LookAtPlayer();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out UfoMovementController ufo))
        {
            _isEntered = true;
            ufoTransform = other.transform;
            _canFire = true;
            if (_shootingCoroutine == null)
            {
                _shootingCoroutine = StartCoroutine(ShootCoroutine());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out UfoMovementController ufo) && _isEntered)
        {
            _isEntered = false;
            _canFire = false;
            if (_shootingCoroutine != null)
            {
                StopCoroutine(_shootingCoroutine);
                _shootingCoroutine = null;
            }
        }
    }

    private void LookAtPlayer()
    {
        if (ufoTransform != null)
        {
            Vector3 directionToUfo = ufoTransform.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(directionToUfo);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, _lookSpeed * Time.deltaTime);
        }
    }

    private IEnumerator ShootCoroutine()
    {
        while (_canFire)
        {
            FireShoot();
            yield return new WaitForSeconds(2);
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void FireShoot()
    {
        GameObject ammo = Instantiate(projectile, barrel.position, barrel.rotation);
        ammo.GetComponent<Rigidbody>().velocity = barrel.forward * 20f;
        Destroy(ammo, 2f);
    }

    private void CloseShoot()
    {
        _canFire = false;
    }
}