using System.Collections;
using System.Collections.Generic;
using Unity.Services.Mediation;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Mediation;


public class MediationManager : MonoBehaviour
{
    [SerializeField] private InterstitialAd _interstitialAd;
    [SerializeField] private RewardedAd _rewardedAd;
    
    public bool IsInitialized { get; private set; }
    public bool IsInterstitialAdLoaded => _interstitialAd.IsLoaded;
    public bool IsRewardedAdLoaded => _rewardedAd.IsLoaded;
    

    public async void Init()
    {
        await UnityServices.InitializeAsync();

        IsInitialized = true;
    }

    public void LoadInterstitialAd()
    {
        if (IsInitialized)
        {
            _interstitialAd.Load();
        }
        else
        {
            Logger.Log("MediationManager :: LoadInterstitialAd :: Mediation SDK is not initialized");
        }
    }

    public void LoadRewardedAd()
    {
        if (IsInitialized)
        {
            _rewardedAd.Load();
        }
        else
        {
            Logger.Log("MediationManager :: LoadRewardedAd :: Mediation SDK is not initialized");
        }
    }

    public void ShowInterstitialAd()
    {
        if (IsInitialized)
        {
            _interstitialAd.Show();
        }
        else
        {
            Logger.Log("MediationManager :: ShowInterstitialAd :: Mediation SDK is not initialized");
        }
    }

    public void ShowRewardedAd()
    {
        if (IsInitialized)
        {
            _rewardedAd.Show();
        }
        else
        {
            Logger.Log("MediationManager :: ShowRewardedAd :: Mediation SDK is not initialized");
        }
    }
}