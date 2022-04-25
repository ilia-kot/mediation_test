using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Mediation;
using UnityEngine;

public class RewardedAd : MonoBehaviour
{
    [SerializeField] private string androidAdUnitId;
    [SerializeField] private string iosAdUnitId;
    [SerializeField] private string testAdUnitId;
    
    IRewardedAd rewardedAd;
    
    public bool IsLoaded { get; private set; }
    
    public void Init()
    {
        //TODO change to defines!
        if (Application.platform == RuntimePlatform.Android)
        {
            rewardedAd = MediationService.Instance.CreateRewardedAd(androidAdUnitId);
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            rewardedAd = MediationService.Instance.CreateRewardedAd(iosAdUnitId);
        }
#if UNITY_EDITOR
        else {
            rewardedAd = MediationService.Instance.CreateRewardedAd(testAdUnitId);
        }
#endif

        rewardedAd.OnLoaded += AdLoaded;
        rewardedAd.OnFailedLoad += AdFailedToLoad;
        rewardedAd.OnShowed += AdShown;
        rewardedAd.OnFailedShow += AdFailedToShow;
        rewardedAd.OnUserRewarded += UserRewarded;
        rewardedAd.OnClosed += AdClosed;
    }

    public void Load()
    {
        rewardedAd.Load();
    }

    public void Show()
    {
        if (rewardedAd.AdState == AdState.Loaded)
        {
            rewardedAd.Show();
        }
    }

    void OnDestroy()
    {
        rewardedAd.OnLoaded -= AdLoaded;
        rewardedAd.OnFailedLoad -= AdFailedToLoad;
        rewardedAd.OnShowed -= AdShown;
        rewardedAd.OnFailedShow -= AdFailedToShow;
        rewardedAd.OnUserRewarded -= UserRewarded;
        rewardedAd.OnClosed -= AdClosed;
        rewardedAd.Dispose();
    }
    
    #region CALLBACKS

// Implement load event callback methods:
    void AdLoaded(object sender, EventArgs args) {
        Debug.Log("Ad loaded.");
// Execute logic for when the ad has loaded
    }

    void AdFailedToLoad(object sender, LoadErrorEventArgs args) {
        Debug.Log("Ad failed to load.");
// Execute logic for the ad failing to load.
    }

// Implement show event callback methods:
    void AdShown(object sender, EventArgs args) {
        Debug.Log("Ad shown successfully.");
// Execute logic for the ad showing successfully.
    }

    void UserRewarded(object sender, RewardEventArgs args) {
        Debug.Log("Ad has rewarded user.");
// Execute logic for rewarding the user.
    }

    void AdFailedToShow(object sender, ShowErrorEventArgs args) {
        Debug.Log("Ad failed to show.");
// Execute logic for the ad failing to show.
    }

    void AdClosed(object sender, EventArgs e) {
        Debug.Log("Ad is closed.");
// Execute logic for the user closing the ad.
    }
    
    #endregion
}