#if  AIG_ANALYTICS_APP_METRICA
using UnityEngine;

namespace Aig.Client.Integration.Runtime.Analytics
{
    public class AppMetricaExt : MonoBehaviour
    {
        public const string VERSION = "3.7.0";

        [SerializeField] public string ApiKey;

        [SerializeField] public bool ExceptionsReporting = true;

        [SerializeField] public uint SessionTimeoutSec = 10;

        [SerializeField] public bool LocationTracking = true;

        [SerializeField] public bool Logs = true;

        [SerializeField] public bool HandleFirstActivationAsUpdate = false;

        [SerializeField] public bool StatisticsSending = true;

        private static bool _isInitialized = false;
        private bool _actualPauseStatus = false;
    
        private static object syncRoot = new Object();

        void SetupMetrica()
        {
            
        }

        private void Awake()
        {
            if (!_isInitialized)
            {
                _isInitialized = true;
                SetupMetrica();
                //DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        private void Start()
        {
            
        }

        private void OnEnable()
        {
            if (ExceptionsReporting)
            {
#if UNITY_5 || UNITY_5_3_OR_NEWER
                Application.logMessageReceived += HandleLog;
#else
            Application.RegisterLogCallback(HandleLog);
#endif
            }
        }

        private void OnDisable()
        {
            if (ExceptionsReporting)
            {
#if UNITY_5 || UNITY_5_3_OR_NEWER
                Application.logMessageReceived -= HandleLog;
#else
            Application.RegisterLogCallback(null);
#endif
            }
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (_actualPauseStatus != pauseStatus)
            {
                _actualPauseStatus = pauseStatus;
                if (pauseStatus)
                {
                    
                }
                else
                {
                   
                }
            }
        }

        private void HandleLog(string condition, string stackTrace, LogType type)
        {
            if (type == LogType.Exception)
            {
                
            }
        }

    }
}
#endif
