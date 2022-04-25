using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Mediation;
using UnityEngine;

public class InterstitialAd : MonoBehaviour
{ 
    [SerializeField] private string androidAdUnitId;
    [SerializeField] private string iosAdUnitId;
    [SerializeField] private string testAdUnitId;

    IInterstitialAd interstitialAd;

    public bool IsLoaded => interstitialAd != null && interstitialAd.AdState == AdState.Loaded;


    public void Init()
    {
        // TODO change to defines?
        if (Application.platform == RuntimePlatform.Android) {
            interstitialAd = MediationService.Instance.CreateInterstitialAd(androidAdUnitId);
        } else if (Application.platform == RuntimePlatform.IPhonePlayer) {
            interstitialAd = MediationService.Instance.CreateInterstitialAd(iosAdUnitId);
        }
#if UNITY_EDITOR
        else {
            interstitialAd = MediationService.Instance.CreateInterstitialAd(testAdUnitId);
        }
#endif

        interstitialAd.OnLoaded += AdLoaded;
        interstitialAd.OnFailedLoad += AdFailedToLoad;
        interstitialAd.OnShowed += AdShown;
        interstitialAd.OnFailedShow += AdFailedToShow;
        interstitialAd.OnClosed += AdClosed;
    }
  
    public void Load()
    {
        interstitialAd.Load();
    }

    public void Show()
    {
        if (interstitialAd.AdState == AdState.Loaded)
        {
            interstitialAd.Show();
        }
    }

    void OnDestroy()
    {
        interstitialAd.OnLoaded -= AdLoaded;
        interstitialAd.OnFailedLoad -= AdFailedToLoad;
        interstitialAd.OnShowed -= AdShown;
        interstitialAd.OnFailedShow -= AdFailedToShow;
        interstitialAd.OnClosed -= AdClosed;
        interstitialAd.Dispose();
    }

    #region CALLBACKS

    // Implement load event callback methods:
    void AdLoaded(object sender, EventArgs args) {
        Debug.Log("Ad loaded.");
        // Execute logic for when the ad has loaded
    }

    void AdFailedToLoad(object sender, LoadErrorEventArgs args) {
        Debug.Log("Ad failed to load.");
        Debug.Log("Error: " + args.Error + " Message: " + args.Message);
        // Execute logic for the ad failing to load.
    }

    // Implement show event callback methods:
    void AdShown(object sender, EventArgs args) {
        Debug.Log("Ad shown successfully.");
        // Execute logic for the ad showing successfully.
    }

    void AdFailedToShow(object sender, ShowErrorEventArgs args) {
        Debug.Log("Ad failed to show.");
        // Execute logic for the ad failing to show.
    }

    private void AdClosed(object sender, EventArgs e) {
        Debug.Log("Ad has closed");
        // Execute logic after an ad has been closed.
    }
    #endregion
  
}