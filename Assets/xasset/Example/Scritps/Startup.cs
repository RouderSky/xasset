using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace xasset.example
{
    [RequireComponent(typeof(Downloader))]
    [RequireComponent(typeof(Scheduler))]
    [RequireComponent(typeof(Recycler))]
    [DisallowMultipleComponent]
    public class Startup : MonoBehaviour
    {
        public ExampleScene startWithScene;
        [SerializeField] private bool loggable = true;
        [SerializeField] private bool fastVerifyMode = true;
        [SerializeField] private bool simulationMode = true;
        [SerializeField] private string baseUpdateURL = $"http://127.0.0.1/{Assets.Bundles}";

        [Conditional("UNITY_EDITOR")]
        private void Awake()
        {
            Assets.SimulationMode = simulationMode;
        }

        private IEnumerator Start()
        {
            DontDestroyOnLoad(gameObject);
            Assets.FastVerifyMode = fastVerifyMode;
            if (!Assets.SimulationMode && !Downloader.SimulationMode)
                Assets.UpdateURL = $"{baseUpdateURL}/{Assets.Platform}/{UpdateInfo.Filename}";

            var initializeAsync = Assets.InitializeAsync();     //todo wht ref 非仿真模式下，这里会失败
            yield return initializeAsync;

            if (false)
            {
                // 获取服务器的更新信息。 
                var getUpdateInfoAsync = Assets.GetUpdateInfoAsync();
                yield return getUpdateInfoAsync;
                if (getUpdateInfoAsync.result == Request.Result.Success)
                {
                    var getVersionsAsync = Assets.GetVersionsAsync(getUpdateInfoAsync.info);
                    yield return getVersionsAsync;
                    if (getVersionsAsync.versions != null)
                    {
                        getVersionsAsync.versions.Save(Assets.GetDownloadDataPath(Versions.Filename));
                        Assets.Versions = getVersionsAsync.versions;
                    }
                }
            }

            yield return Asset.LoadAsync(MessageBox.Filename, typeof(GameObject));
            yield return Asset.InstantiateAsync(LoadingScreen.Filename);
            LoadingScreen.Instance.SetVisible(false);
            Scene.LoadAsync(startWithScene.ToString());
        }

        private void Update()
        {
            Logger.Enabled = loggable;
        }
    }
}