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
        [SerializeField] private TextMeshProUGUI interstitialStatus;

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
            canvasMainMenu.SetActive(true);
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

        void OnDestroy()
        {
            mediationManager.OnInterstitialAdCompleted -= OnInterstitialCompleted;
            mediationManager.OnUserRewarded -= OnUserRewarded;
        }

        void OnInterstitialCompleted(string status)
        {
            canvasMainMenu.SetActive(false);
            canvasPopup.SetActive(true);
            
            interstitialStatus.text = status;
        }

        void OnUserRewarded()
        {
            score++;
            textScore.text = "Score: " + score;
        }
    }
}