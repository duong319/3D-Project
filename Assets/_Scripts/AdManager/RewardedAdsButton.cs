using UnityEngine;
using UnityEngine.Advertisements;
using System.Security.Cryptography;
using System.Collections;
using System;

public enum Rewardtype
{
    None,
    FreeCoins,
}

public class RewardedAdsButton : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public static RewardedAdsButton Instance;
    [SerializeField] string _androidAdUnitId = "Rewarded_Android";
    [SerializeField] string _iOSAdUnitId = "Rewarded_iOS";
    string _adUnitId = null;
    public Action onAdCompleted;
    private Rewardtype currentReward = Rewardtype.None;

    void Awake()
    {
        if (Instance == null) Instance = this;

#if UNITY_IOS
    _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif



    }


    public void LoadAd(Rewardtype rewardType)
    {
        currentReward = rewardType;
        Advertisement.Load(_adUnitId, this);
    }


    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        AudioManager.Instance.Play("Btn");
        Advertisement.Show(adUnitId, this);
    }


    public void ShowAd()
    {
        Advertisement.Show(_adUnitId, this);
    }


    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            switch (currentReward)
            {
                case Rewardtype.FreeCoins:
                    AudioManager.Instance.Play("GetFreeCoin");
                    CurrencyManager.Instance.AddCoins(500);
                    break;

                case Rewardtype.None:
                    onAdCompleted?.Invoke();
                    onAdCompleted = null;
                    break;
            }
        }
        currentReward = Rewardtype.None;
        AchievementManager.Instance.AddProgress(AchievementType.WatchAd, 1);
    }


    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        // Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");   
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        // Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");    
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }

    public void FreeCoin()
    {
        LoadAd(Rewardtype.FreeCoins);
    }
}
