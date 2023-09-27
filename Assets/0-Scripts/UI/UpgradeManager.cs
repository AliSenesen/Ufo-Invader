using _0_Scripts.Enums;
using _0_Scripts.Events;
using _0_Scripts.Particle;
using TMPro;
using Ufo;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private ParticleManager particleManager;
    [SerializeField] private GameObject ufo;
    [SerializeField] private TextMeshProUGUI totalMoneyText;
    [SerializeField] private TextMeshProUGUI menuTotalMoneyText;
    [SerializeField] private int totalMoney;
    


    [Header("SpeedButton")] [SerializeField]
    private Button speedUpButton;

    [SerializeField] private TextMeshProUGUI speedLevelText;
    [SerializeField] private TextMeshProUGUI speedCostText;
    private int _speedUpCost = 100;
    private int _currentSpeedLevel = 1;


    [Header("PullForceButton")] [SerializeField]
    private Button pullForceButton;

    [SerializeField] private TextMeshProUGUI pullForceLevelText;
    [SerializeField] private TextMeshProUGUI pullForceCostText;
    private int _pullForceCost = 100;
    private int _currentPullLevel = 1;


    [Header("ShieldUpButton")] [SerializeField]
    private Button shieldUpButton;

    [SerializeField] private TextMeshProUGUI shieldLevelText;
    [SerializeField] private TextMeshProUGUI shieldCostText;
    private int _shieldCost = 100;

    private int _currentShieldLevel = 1;

    #region Keys
    private const string MoneyKey = "Money";
    private const string SpeedLevelKey = "SpeedLevel";
    private const string PullLevelKey = "PullLevel";
    private const string ShieldLevelKey = "ShieldLevel";
    private const string SpeedCostKey = "SpeedCost";
    private const string PullCostKey = "PullCost";
    private const string ShieldCostKey = "ShieldCost";
    

    #endregion
   

    private void Awake()
    {
        InitData();

        totalMoneyText.text = totalMoney.ToString();
        menuTotalMoneyText.text = totalMoneyText.text;

        speedCostText.text = _speedUpCost.ToString();
        pullForceCostText.text = _pullForceCost.ToString();
        shieldCostText.text = _shieldCost.ToString();

        UpdateData();
    }

    private void OnDestroy()
    {
        SaveDatas();
    }

    public void SpeedUpButton()
    {
        if (totalMoney >= _speedUpCost)
        {
            totalMoney -= _speedUpCost;
            _currentSpeedLevel++;
            _speedUpCost += 100;
            UpdateUI();
            UfoMovementController.instance.MovementSpeed++;
            particleManager.PlayParticle(ParticleTypes.Upgrade,ufo.transform);
            AudioManager.instance.OnPlaySound(AudioStates.Upgrade,false);
        }
    }

    public void PullForceButton()
    {
        if (totalMoney >= _pullForceCost)
        {
            totalMoney -= _pullForceCost;
            _currentPullLevel++;
            _pullForceCost += 100;
            UpdateUI();
            UfoMagnetController.instance.PullForce += 10;
            particleManager.PlayParticle(ParticleTypes.Upgrade,ufo.transform);
            AudioManager.instance.OnPlaySound(AudioStates.Upgrade,false);
        }
    }

    public void ShieldUpButton()
    {
        if (totalMoney >= _shieldCost)
        {
            totalMoney -= _shieldCost;
            _currentShieldLevel++;
            _shieldCost += 100;
            UpdateUI();
            UfoShieldBarController.instance.MaxShield += 20;
            particleManager.PlayParticle(ParticleTypes.Upgrade,ufo.transform);
            AudioManager.instance.OnPlaySound(AudioStates.Upgrade,false);
          
           
        }
    }

    public void AddCoinMoney(int coinValue)
    {
        totalMoney += coinValue;
        UpdateUI();
    }

    public void AddRewardMoney(int winReward)
    {
        totalMoney += winReward;
        UpdateUI();
    }

    public void AddExtraReward( )
    {
        AdManager.instance.ShowRewardedAd(null,null);
        AddRewardMoney(200);
        UpdateUI();
        
       
    }

    private void UpdateUI()
    {
        totalMoneyText.text = totalMoney.ToString();
        menuTotalMoneyText.text = totalMoneyText.text;

        speedUpButton.GetComponentInChildren<TextMeshProUGUI>().text =
            "Level " + _currentSpeedLevel.ToString() + "\nCost: " + _speedUpCost.ToString();
        speedLevelText.text = "Level " + _currentSpeedLevel.ToString();
        speedCostText.text = _speedUpCost.ToString();

        pullForceButton.GetComponentInChildren<TextMeshProUGUI>().text =
            "Level " + _currentPullLevel.ToString() + "\nCost: " + _pullForceCost.ToString();
        pullForceLevelText.text = "Level " + _currentPullLevel.ToString();
        pullForceCostText.text = _pullForceCost.ToString();

        shieldUpButton.GetComponentInChildren<TextMeshProUGUI>().text =
            "Level" + _currentShieldLevel.ToString() + "\nCost:" + _shieldCost.ToString();
        shieldLevelText.text = "Level " + _currentShieldLevel.ToString();
        shieldCostText.text = _shieldCost.ToString();
    }

    private void InitData()
    {
        totalMoney = PlayerPrefs.GetInt(MoneyKey, totalMoney);
        _currentSpeedLevel = PlayerPrefs.GetInt(SpeedLevelKey, _currentSpeedLevel);
        _currentPullLevel = PlayerPrefs.GetInt(PullLevelKey, _currentPullLevel);
        _currentShieldLevel = PlayerPrefs.GetInt(ShieldLevelKey, _currentShieldLevel);
        _speedUpCost = PlayerPrefs.GetInt(SpeedCostKey, _speedUpCost);
        _pullForceCost = PlayerPrefs.GetInt(PullCostKey, _pullForceCost);
        _shieldCost = PlayerPrefs.GetInt(ShieldCostKey, _shieldCost);
    }

    private void UpdateData()
    {
        UfoMovementController.instance.MovementSpeed += _currentSpeedLevel - 1;
        UfoMagnetController.instance.PullForce += 10 * (_currentPullLevel - 1);
        UfoShieldBarController.instance.MaxShield += 20 * (_currentShieldLevel - 1);
    }

    private void SaveDatas()
    {
        PlayerPrefs.SetInt(MoneyKey, totalMoney);
        PlayerPrefs.SetInt(SpeedLevelKey, _currentSpeedLevel);
        PlayerPrefs.SetInt(PullLevelKey, _currentPullLevel);
        PlayerPrefs.SetInt(ShieldLevelKey, _currentShieldLevel);
        PlayerPrefs.SetInt(SpeedCostKey, _speedUpCost);
        PlayerPrefs.SetInt(PullCostKey, _pullForceCost);
        PlayerPrefs.SetInt(ShieldCostKey, _shieldCost);
      
        PlayerPrefs.Save();
    }
}