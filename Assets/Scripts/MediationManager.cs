using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Mediation;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Mediation;
using Unity.VisualScripting;

namespace mediation_test
{

    public class MediationManager : MonoBehaviour
    {
        [SerializeField] private string GAME_ID_IOS = "4720904";
        [SerializeField] private string GAME_ID_ANDROID = "4720905";
        [SerializeField] private InterstitialAd _interstitialAd;
        [SerializeField] private RewardedAd _rewardedAd;

        public bool IsInitialized { get; private set; }
        public bool IsInterstitialAdLoaded => _interstitialAd.IsLoaded;
        public bool IsRewardedAdLoaded => _rewardedAd.IsLoaded;

        public delegate void InterstitialAdCompleted(string info);
        public delegate void RewardedAdCompleted();

        public event InterstitialAdCompleted OnInterstitialAdCompleted;
        public event RewardedAdCompleted OnUserRewarded;


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

                _interstitialAd.Interstitial.OnFailedShow += OnInterstitialFailedShow;
                _interstitialAd.Interstitial.OnClosed += OnInterstitialClosed;
                _interstitialAd.Interstitial.OnShowed += OnInterstitialShowed;
                
                _rewardedAd.Rewarded.OnUserRewarded += OnRewardedUserRewarded;

                IsInitialized = true;
            }
        }

        public void LoadInterstitialAd()
        {
            if (IsInitialized)
                _interstitialAd.Load();
            else
                Logger.Log("MediationManager :: LoadInterstitialAd :: Mediation SDK is not initialized");
        }

        public void LoadRewardedAd()
        {
            if (IsInitialized)
                _rewardedAd.Load();
            else
                Logger.Log("MediationManager :: LoadRewardedAd :: Mediation SDK is not initialized");
        }

        public void ShowInterstitialAd()
        {
            if (IsInitialized)
                _interstitialAd.Show();
            else
                Logger.Log("MediationManager :: ShowInterstitialAd :: Mediation SDK is not initialized");
        }

        public void ShowRewardedAd()
        {
            if (IsInitialized)
                _rewardedAd.Show();
            else
                Logger.Log("MediationManager :: ShowRewardedAd :: Mediation SDK is not initialized");
        }

        void OnDestroy()
        {
            if (IsInitialized)
            {
                if (_interstitialAd != null)
                {
                    _interstitialAd.Interstitial.OnFailedShow -= OnInterstitialFailedShow;
                    _interstitialAd.Interstitial.OnClosed -= OnInterstitialClosed;
                    _interstitialAd.Interstitial.OnShowed -= OnInterstitialShowed;
                }

                if (_rewardedAd != null)
                {
                    _rewardedAd.Rewarded.OnUserRewarded += OnRewardedUserRewarded;
                }
            }
        }

        #region CALLBACKS
        
        // Interstitial Ad
        private void OnInterstitialShowed(object sender, EventArgs e)
        {
            OnInterstitialAdCompleted("Interstitial Ad Showed");
        }

        private void OnInterstitialClosed(object sender, EventArgs e)
        {
            OnInterstitialAdCompleted("Interstitial Ad Closed");
        }

        private void OnInterstitialFailedShow(object sender, ShowErrorEventArgs e)
        {
            OnInterstitialAdCompleted("Interstitial Ad failed to show");
        }
        
        // Rewarded Ad
        private void OnRewardedUserRewarded(object sender, RewardEventArgs e)
        {
            OnUserRewarded();
        }
        
        #endregion

    }
}