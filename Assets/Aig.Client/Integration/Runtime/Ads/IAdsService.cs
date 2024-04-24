using System;

namespace Aig.Client.Integration.Runtime.Ads
{
    public interface IAdsService
    {
        void RunService();

        bool IsInterstitialVideoReady();
        bool IsRewardVideoReady();

        bool ShowVideo(bool rewarded, string placement, LevelInfo levelInfo, Action rewardCallback = null,
            bool ignoreTimeout = false);

        void ShowBanner();
        void HideBanner();

        Action OnAdNotReady { get; set; }
        Action<bool> OnMuteGame { get; set; }

        Func<bool> NoAdFunc { get; set; }
        Func<bool> AdShowFunc { get; set; }

        void LoadInterstitial();
        void LoadRewardedAd();
        void CreteBanner();

        Action<string, string, string, LevelInfo, int> OnVideoAdsWatch{ get; set; }
        Action<string, string, string> OnVideoAdsStarted{ get; set; }
        Action<string, string, string> OnVideoAdsAvailable{ get; set; }
    }
}