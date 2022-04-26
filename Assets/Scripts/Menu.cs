using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mediation_test
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private MediationManager mediationManager;

        void Start()
        {
            mediationManager.OnInterstitialAdCompleted += OnInterstitialCompleted;
            mediationManager.OnRewardedAdCompleted += OnRewardedAdCompleted;
        }

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
            ;
        }

        void OnDestroy()
        {
            mediationManager.OnInterstitialAdCompleted -= OnInterstitialCompleted;
            mediationManager.OnRewardedAdCompleted -= OnRewardedAdCompleted;
        }

        void OnInterstitialCompleted(string status)
        {
            Logger.Log("SHOW POPUP :: " + status);
        }

        void OnRewardedAdCompleted(string status)
        {
            Logger.Log("SHOW POPUP :: " + status);
        }
    }
}