using System;
using System.Collections;
using System.Collections.Generic;
using Ufo;
using UnityEngine;

public class DangerZoneController : MonoBehaviour
{
    [SerializeField] private Material zoneMaterial;
    [SerializeField] private Material redZoneMaterial;
    [SerializeField] private Renderer _renderer;


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out UfoMovementController ufo))
        {
            _renderer.material = redZoneMaterial;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out UfoMovementController ufo))
        {
            _renderer.material = zoneMaterial;
        }
    }
}