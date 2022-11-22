using UnityEngine;

namespace xasset.editor
{
    public static class Initializer
    {
        // todo wht ref ��������ᱻ�Զ�����
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void RuntimeInitializeOnLoad()
        {
            Assets.Platform = Settings.Platform;
            // todo wht ref ����ģʽ
            if (Assets.SimulationMode)
            {
                InitializeRequest.CreateHandler = InitializeRequestHandlerSimulation.CreateInstance;
                AssetRequest.CreateHandler = AssetRequestHandlerSimulation.CreateInstance;
                SceneRequest.CreateHandler = SceneRequestHandlerSimulation.CreateInstance;
            }

            // todo wht ref ���ط���ģʽ
            if (!Downloader.SimulationMode) return;
            Assets.UpdateURL = $"{Assets.Protocol}{Settings.GetDataPath(UpdateInfo.Filename)}";
            Assets.DownloadURL = $"{Assets.Protocol}{Settings.PlatformDataPath}";
        }
    }
}