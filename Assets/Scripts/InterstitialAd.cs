using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Mediation;
using UnityEngine;

namespace mediation_test
{
    public class InterstitialAd : MonoBehaviour
    {
        [SerializeField] private string androidAdUnitId;
        [SerializeField] private string iosAdUnitId;
        [SerializeField] private string testAdUnitId;

        private IInterstitialAd interstitialAd;

        public IInterstitialAd Interstitial => interstitialAd;
        public bool IsLoaded => interstitialAd != null && interstitialAd.AdState == AdState.Loaded;
        

        public void Init()
        {
            if (Application.isEditor)
                interstitialAd = MediationService.Instance.CreateInterstitialAd(testAdUnitId);
            else if (Application.platform == RuntimePlatform.Android)
                interstitialAd = MediationService.Instance.CreateInterstitialAd(androidAdUnitId);
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
                interstitialAd = MediationService.Instance.CreateInterstitialAd(iosAdUnitId);
            
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

        void AdLoaded(object sender, EventArgs args)
        {
            Logger.Log("InterstitialAd :: AdLoaded :: AdUnitId :: " + interstitialAd.AdUnitId);
        }

        void AdFailedToLoad(object sender, LoadErrorEventArgs args)
        {
            Logger.Log("InterstitialAd :: AdFailedToLoad :: AdUnitId :: " + interstitialAd.AdUnitId + " Error : " + args.Error + " Message: " + args.Message);
        }

        void AdShown(object sender, EventArgs args)
        {
            Logger.Log("InterstitialAd :: AdShown :: AdUnitId :: " + interstitialAd.AdUnitId);
        }

        void AdFailedToShow(object sender, ShowErrorEventArgs args)
        {
            Logger.Log("InterstitialAd :: AdFailedToShow :: AdUnitId :: " + interstitialAd.AdUnitId + " Error : " + args.Error + " Message: " + args.Message);
        }

        private void AdClosed(object sender, EventArgs args)
        {
            Logger.Log("InterstitialAd :: AdClosed :: AdUnitId :: " + interstitialAd.AdUnitId);
        }

        #endregion

    }
}
