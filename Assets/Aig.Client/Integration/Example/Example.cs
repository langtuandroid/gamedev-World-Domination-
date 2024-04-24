using System;
using Aig.Client.Integration.Runtime.Analytics;
using Aig.Client.Integration.Runtime.Subsystem;
using UnityEngine;

namespace Aig.Client.Integration.Example
{
    public class Example : MonoBehaviour
    {
        private void Start()
        {
            //IntegrationSubsystem.Instance.Run();
        }

        public void OnShowBannerButtonClick()
        {
            Debug.Log("OnShowBannerButtonClick");
            IntegrationSubsystem.Instance.AdsService.ShowBanner();
        }

        public void OnShowRewardButtonClick()
        {
            Debug.Log("OnShowRewardButtonClick");
            IntegrationSubsystem.Instance.AdsService.ShowVideo(true, "Main", new LevelInfo());
        }

        public void OnShowInterButtonClick()
        {
            Debug.Log("OnShowInterButtonClick");
            IntegrationSubsystem.Instance.AdsService.ShowVideo(false, "Main", new LevelInfo());
        }

        public void OnSendLevelStartButtonClick()
        {
            Debug.Log("OnSendLevelStartButtonClick");
            IntegrationSubsystem.Instance.AnalyticsService.LevelStart(new LevelInfo(), "level0001");
        }

        public void OnSendLevelFinishButtonClick()
        {
            Debug.Log("OnSendLevelFinishButtonClick");
            IntegrationSubsystem.Instance.AnalyticsService.LevelFinish(new LevelInfo(), "level0001", "finish", 100, 1);
        }
    }
}
