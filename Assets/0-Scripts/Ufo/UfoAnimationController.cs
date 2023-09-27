using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UfoAnimationController : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float moveDistance;
    [SerializeField] private float animationDuration;
    
    private void Start()
    {
        FlyAnimation();
        StartCoroutine(UfoRotator());
    }

    private IEnumerator UfoRotator()
    {
        while (true)
        {
            transform.Rotate(Vector3.up * (rotateSpeed * Time.deltaTime));
            yield return null;
        }
    }

    private void FlyAnimation()
    {
        transform.DOMoveY(transform.position.y - moveDistance, animationDuration)
            .SetEase(Ease.InOutQuad)
            .SetLoops(-1, LoopType.Yoyo); 
    }
    
}
