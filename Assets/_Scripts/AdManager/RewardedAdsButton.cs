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
    string _adUnitId = null; // This will remain null for unsupported platforms
    public Action onAdCompleted;
    private Rewardtype currentReward = Rewardtype.None;

    void Awake()
    {
        if (Instance == null) Instance = this;
        // Get the Ad Unit ID for the current platform:
#if UNITY_IOS
    _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif



    }

    // Call this public method when you want to get an ad ready to show.
    public void LoadAd(Rewardtype rewardType)
    {

        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        currentReward = rewardType;
        Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }

    // If the ad successfully loads, add a listener to the button and enable it:
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + adUnitId);

        Advertisement.Show(adUnitId, this);
    }

    // Implement a method to execute when the user clicks the button:
    public void ShowAd()
    {
        // Disable the button:

        // Then show the ad:
        Advertisement.Show(_adUnitId, this);
    }

    // Implement the Show Listener's OnUnityAdsShowComplete callback method to determine if the user gets a reward:
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Unity Ads Rewarded Ad Completed");
            switch (currentReward)
            {
                case Rewardtype.FreeCoins:
                    Debug.Log("Get Free Coin");
                    AudioManager.Instance.Play("GetFreeCoin");
                    CurrencyManager.Instance.AddCoins(500);
                    break;

                case Rewardtype.None:
                    onAdCompleted?.Invoke();
                    onAdCompleted = null;
                    Debug.Log("Chest");
                    break;
            }
        }
        currentReward = Rewardtype.None;
        AchievementManager.Instance.AddProgress(AchievementType.WatchAd, 1);
    }


    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");   
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");    
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }

    public void FreeCoin()
    {
        LoadAd(Rewardtype.FreeCoins);
    }
}
