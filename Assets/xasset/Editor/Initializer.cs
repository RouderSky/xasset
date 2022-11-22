using UnityEngine;

namespace xasset.editor
{
    public static class Initializer
    {
        // todo wht ref 这个函数会被自动调用
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void RuntimeInitializeOnLoad()
        {
            Assets.Platform = Settings.Platform;
            // todo wht ref 仿真模式
            if (Assets.SimulationMode)
            {
                InitializeRequest.CreateHandler = InitializeRequestHandlerSimulation.CreateInstance;
                AssetRequest.CreateHandler = AssetRequestHandlerSimulation.CreateInstance;
                SceneRequest.CreateHandler = SceneRequestHandlerSimulation.CreateInstance;
            }

            // todo wht ref 下载仿真模式
            if (!Downloader.SimulationMode) return;
            Assets.UpdateURL = $"{Assets.Protocol}{Settings.GetDataPath(UpdateInfo.Filename)}";
            Assets.DownloadURL = $"{Assets.Protocol}{Settings.PlatformDataPath}";
        }
    }
}