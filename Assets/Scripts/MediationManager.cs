using System.Collections;
using System.Collections.Generic;
using Unity.Services.Mediation;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Mediation;
using Unity.VisualScripting;


public class MediationManager : MonoBehaviour
{
    [SerializeField] private string GAME_ID_IOS = "4720904";
    [SerializeField] private string GAME_ID_ANDROID = "4720905";
    [SerializeField] private InterstitialAd _interstitialAd;
    [SerializeField] private RewardedAd _rewardedAd;
    
    public bool IsInitialized { get; private set; }
    public bool IsInterstitialAdLoaded => _interstitialAd.IsLoaded;
    public bool IsRewardedAdLoaded => _rewardedAd.IsLoaded;
    

    public async void Init()
    {
        if (!IsInitialized)
        {
            // Initialize package with a custom Game ID
            InitializationOptions options = new InitializationOptions();
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                options.SetGameId(GAME_ID_IOS);
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
                options.SetGameId(GAME_ID_ANDROID);
            }
            await UnityServices.InitializeAsync(options);
            
            _interstitialAd.Init();
            _rewardedAd.Init();
            
            IsInitialized = true;
        }
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