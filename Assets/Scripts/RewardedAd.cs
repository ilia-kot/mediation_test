using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Mediation;
using UnityEngine;


namespace mediation_test
{
    public class RewardedAd : MonoBehaviour
    {
        [SerializeField] private string androidAdUnitId;
        [SerializeField] private string iosAdUnitId;
        [SerializeField] private string testAdUnitId;

        private IRewardedAd rewardedAd;

        public IRewardedAd Rewarded => rewardedAd;
        public bool IsLoaded => rewardedAd != null && rewardedAd.AdState == AdState.Loaded;


        public void Init()
        {
            if (Application.isEditor)
                rewardedAd = MediationService.Instance.CreateRewardedAd(testAdUnitId);
            else if (Application.platform == RuntimePlatform.Android)
                rewardedAd = MediationService.Instance.CreateRewardedAd(androidAdUnitId);
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
                rewardedAd = MediationService.Instance.CreateRewardedAd(iosAdUnitId);

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

        void AdLoaded(object sender, EventArgs args)
        {
            Logger.Log("RewardedAd :: AdLoaded");
        }

        void AdFailedToLoad(object sender, LoadErrorEventArgs args)
        {
            Logger.Log("RewardedAd :: AdFailedToLoad :: Error : " + args.Error + " Message: " + args.Message);
        }

        void AdShown(object sender, EventArgs args)
        {
            Logger.Log("RewardedAd :: AdShown");
        }

        void UserRewarded(object sender, RewardEventArgs args)
        {
            Logger.Log("RewardedAd :: UserRewarded");
        }

        void AdFailedToShow(object sender, ShowErrorEventArgs args)
        {
            Logger.Log("RewardedAd :: AdFailedToShow :: Error : " + args.Error + " Message: " + args.Message);
        }

        void AdClosed(object sender, EventArgs args)
        {
            Logger.Log("RewardedAd :: AdClosed");
        }

        #endregion
    }
}