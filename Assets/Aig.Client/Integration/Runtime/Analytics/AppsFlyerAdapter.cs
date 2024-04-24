using System.Collections.Generic;
using System.Globalization;
using Aig.Client.Integration.Runtime.Settings;
#if AIG_ANALYTICS_APPS_FLYER
#endif
using UnityEngine;

namespace Aig.Client.Integration.Runtime.Analytics
{
    public class AppsFlyerAdapter : MonoBehaviour, IAnalyticsAdapter
    {
        public AnalyticType AnalyticType { get; } = AnalyticType.AppsFlyer;

        public void Initialize(IntegrationSettings settings)
        {
        }

        public void VideoAdsAvailable(string adType, string placement, string result){}

        public void VideoAdsStared(string adType, string placement, string result){}

        public void VideoAdsWatch(string adType, string placement, string result, LevelInfo levelInfo){}

        public void PaymentSucceed(string inAppId, string currency, float price, string inAppType)
        {

        }

        public void RateUs(string showReason, int rateResult){}

        public void LevelStart(LevelInfo levelInfo, string gameMode){}

        public void LevelFinish(LevelInfo levelInfo, string gameMode, string result, int time, int progress, int continueCount){}

        public void StageStart(LevelInfo levelInfo, StageInfo stageInfo){}

        public void StageFinish(LevelInfo levelInfo, StageInfo stageInfo, string result, int time, int progress, int continueCount){}

        public void Tutorial(string stepName){}

        public void Technical(string stepName, bool firstStart){}

        public void LevelUp(int level){}

        public void Errors(string type, string place, string errorName){}

        public void SkinUnlock(string skinType, string skinName, string skinRarity, string unlockType){}

        public void AbGroup(string eventName, string abGroupName){}

        public void CustomEvent(string eventName, Dictionary<string, object> eventParams, bool sendInstant)
        {
            var param = new Dictionary<string, string>();
            if (eventParams != null)
            {
                foreach (var keyValuePair in eventParams)
                {
                    param.Add(keyValuePair.Key, keyValuePair.Value.ToString());
                }
            }
        }
    }
}
