using System;
using _0_Scripts.Events;
using UnityEngine;
using UnityEngine.UI;

public class UfoShieldBarController : MonoBehaviour
{
    public static UfoShieldBarController instance;
    public float MaxShield = 100f;

    [SerializeField] private Slider shieldBarSlider;
    [SerializeField] private float smoothTime = 0.2f;

    private float _currentShield;
    private float _targetShield;
    private float _smoothVelocity;

    private void OnEnable()
    {
        GameEvents.onLevelChange.AddListener(OnLevelChange);
    }

    private void OnDisable()
    {
        GameEvents.onLevelChange.RemoveListener(OnLevelChange);
    }

    private void Awake()
    {
        instance = this;

        shieldBarSlider.maxValue = MaxShield;
        shieldBarSlider.value = shieldBarSlider.maxValue;
        _currentShield = MaxShield;
        _targetShield = _currentShield;
    }


    private void Update()
    {
        _currentShield = Mathf.SmoothDamp(_currentShield, _targetShield, ref _smoothVelocity, smoothTime);
        shieldBarSlider.value = Mathf.Round(_currentShield * MaxShield) / shieldBarSlider.maxValue;

        if (_targetShield <= 0)
        {
            GameEvents.onFail.Invoke();
        }

        if (shieldBarSlider.maxValue < MaxShield)
        {
            shieldBarSlider.maxValue = MaxShield;
            shieldBarSlider.value = shieldBarSlider.maxValue;
            _currentShield = shieldBarSlider.value;
            _targetShield = _currentShield;
        }
    }

    public void IncreaseShield(float shieldAmount)
    {
        float missingShield = MaxShield - _currentShield;
        float shieldIncrease = Mathf.Min(shieldAmount, missingShield);
        _currentShield = Mathf.Clamp(_currentShield + shieldIncrease, 0, MaxShield);
        _targetShield = _currentShield;
        shieldBarSlider.value = _targetShield;

        if (MaxShield < _targetShield)
        {
            MaxShield = _targetShield;
            shieldBarSlider.maxValue = MaxShield;
        }
    }

    public void DecreaseShield(float damageAmount)
    {
        float newTargetShield = Mathf.Clamp(_targetShield - damageAmount, 0, MaxShield);
        _targetShield = newTargetShield;
        shieldBarSlider.value = _targetShield;
    }

    private void OnLevelChange()
    {
        shieldBarSlider.maxValue = MaxShield;
        shieldBarSlider.value = shieldBarSlider.maxValue;
        _currentShield = MaxShield;
        _targetShield = _currentShield;

        if (shieldBarSlider.maxValue < MaxShield)
        {
            shieldBarSlider.maxValue = MaxShield;
        }
    }
}