using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace mediation_test
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private GameObject canvasMainMenu;
        [SerializeField] private GameObject canvasPopup;
        [SerializeField] private MediationManager mediationManager;
        [SerializeField] private TextMeshProUGUI textScore;

        private int score = 0;

        
        public void InitMediation()
        {
            mediationManager.Init();
        }

        public void LoadInterstitialAd()
        {
            mediationManager.LoadInterstitialAd();
        }

        public void LoadRewardedAd()
        {
            mediationManager.LoadRewardedAd();
        }

        public void ShowInterstitialAd()
        {
            mediationManager.ShowInterstitialAd();
        }

        public void ShowRewardedAd()
        {
            mediationManager.ShowRewardedAd();
        }
        
        public void ClosePopup()
        {
            canvasPopup.SetActive(false);
        }

        void Start()
        {
            mediationManager.OnInterstitialAdCompleted += OnInterstitialCompleted;
            mediationManager.OnUserRewarded += OnUserRewarded;
        }

        void Awake()
        {
            canvasMainMenu.SetActive(true);
            canvasPopup.SetActive(false);
        }

        /*
        void OnGUI()
        {
            GUI.BeginGroup(new Rect(0, 0, 500, 500));

            if (GUI.Button(new Rect(0, 0, 200, 50), "Init Mediation SDK"))
            {
                mediationManager.Init();
            }

            if (GUI.Button(new Rect(0, 50, 200, 50), "Load Interstitial Ad"))
            {
                mediationManager.LoadInterstitialAd();
            }

            if (GUI.Button(new Rect(0, 100, 200, 50), "Load Rewarded Ad"))
            {
                mediationManager.LoadRewardedAd();
            }

            if (GUI.Button(new Rect(0, 150, 200, 50), "Show Interstitial Ad"))
            {
                mediationManager.ShowInterstitialAd();
            }

            if (GUI.Button(new Rect(0, 200, 200, 50), "Show Rewarded Ad"))
            {
                mediationManager.ShowRewardedAd();
            }

            string info = "Mediation SDK Status" +
                          "\nIsInitialized :: " + mediationManager.IsInitialized +
                          "\nIsInterstitialAdLoaded :: " + mediationManager.IsInterstitialAdLoaded +
                          "\nIsRewardedAdLoaded :: " + mediationManager.IsRewardedAdLoaded;

            GUI.Label(new Rect(0, 250, 200, 200), info);

            GUI.EndGroup();
        }
        */

        void OnDestroy()
        {
            mediationManager.OnInterstitialAdCompleted -= OnInterstitialCompleted;
            mediationManager.OnUserRewarded -= OnUserRewarded;
        }

        void OnInterstitialCompleted(string status)
        {
            canvasPopup.SetActive(false);
        }

        void OnUserRewarded()
        {
            score++;
            textScore.text = "Score: " + score;
        }
    }
}