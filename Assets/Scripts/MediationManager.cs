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

        public delegate void RewardedAdCompleted(string info);

        public event InterstitialAdCompleted OnInterstitialAdCompleted;
        public event RewardedAdCompleted OnRewardedAdCompleted;


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

                _interstitialAd.Interstitial.OnShowed += OnInterstitialShowed;
                _interstitialAd.Interstitial.OnFailedShow += OnInterstitialFailedShow;
                _interstitialAd.Interstitial.OnClosed += OnInterstitialClosed;

                _rewardedAd.Rewarded.OnShowed += OnRewardedAdShown;
                _rewardedAd.Rewarded.OnFailedShow += OnRewardedAdFailedToShow;
                _rewardedAd.Rewarded.OnUserRewarded += OnRewardedUserRewarded;
                _rewardedAd.Rewarded.OnClosed += OnRewardedAdClosed;

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
                    _interstitialAd.Interstitial.OnShowed -= OnInterstitialShowed;
                    _interstitialAd.Interstitial.OnFailedShow -= OnInterstitialFailedShow;
                    _interstitialAd.Interstitial.OnClosed -= OnInterstitialClosed;
                }

                if (_rewardedAd != null)
                {
                    _rewardedAd.Rewarded.OnShowed += OnRewardedAdShown;
                    _rewardedAd.Rewarded.OnFailedShow += OnRewardedAdFailedToShow;
                    _rewardedAd.Rewarded.OnUserRewarded += OnRewardedUserRewarded;
                    _rewardedAd.Rewarded.OnClosed += OnRewardedAdClosed;
                }
            }
        }

        #region CALLBACKS

        private void OnInterstitialClosed(object sender, EventArgs e)
        {
            OnInterstitialAdCompleted("Interstitial Ad Closed");
        }

        private void OnInterstitialFailedShow(object sender, ShowErrorEventArgs e)
        {
            OnInterstitialAdCompleted("Interstitial Ad failed to show");
        }

        private void OnInterstitialShowed(object sender, EventArgs e)
        {
            OnInterstitialAdCompleted("Interstitial Ad showed");
        }

        private void OnRewardedAdClosed(object sender, EventArgs e)
        {
            OnRewardedAdCompleted("Rewarded Ad Closed");
        }

        private void OnRewardedUserRewarded(object sender, RewardEventArgs e)
        {
            OnRewardedAdCompleted("User Rewarded");
        }

        private void OnRewardedAdFailedToShow(object sender, ShowErrorEventArgs e)
        {
            OnRewardedAdCompleted("Rewarded Ad failed to show");
        }

        private void OnRewardedAdShown(object sender, EventArgs e)
        {
            OnRewardedAdCompleted("Rewarded Ad Shown");
        }

        #endregion

    }
}