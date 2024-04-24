using Aig.Client.Integration.Runtime.Subsystem;

namespace Aig.Client.Integration.Runtime.Ads
{
    public class AdsAnalyticsService
    {
        public virtual void Init()
        {
            var adsService = IntegrationSubsystem.Instance.AdsService;

            adsService.OnVideoAdsWatch += OnVideoAdsWatch;
            adsService.OnVideoAdsStarted += OnVideoAdsStarted;
            adsService.OnVideoAdsAvailable += OnVideoAdsAvailable;
        }

        private void OnVideoAdsWatch(string adType, string placement, string result, LevelInfo levelInfo, int priority)
        {
            IntegrationSubsystem.Instance.AnalyticsService.VideoAdsWatch(adType, placement, result, levelInfo, priority);
        }

        private void OnVideoAdsStarted(string adType, string placement, string result)
        {
            IntegrationSubsystem.Instance.AnalyticsService.VideoAdsStarted(adType, placement, result);
        }

        private void OnVideoAdsAvailable(string adType, string placement, string result)
        {
            IntegrationSubsystem.Instance.AnalyticsService.VideoAdsAvailable(adType, placement, result);
        }

    }
}
