using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private MediationManager mediationManager;
    
    
    void OnGUI()
    {
        GUI.BeginGroup(new Rect(0,0,500,500));

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

        GUI.EndGroup();;
    }
}
