using Client.Code.Core.Config;
using Client.Code.Core.Scene;
using Client.Code.Core.ServiceLocator;
using UnityEngine;

namespace Client.Code.Bootstrap
{
    public class BootstrapInstaller : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);

            var configsController = new ConfigsController();
            configsController.Initialize();
            ServiceLocator.Register<IConfigsProvider>(configsController);

            var sceneLoader = new SceneLoader(configsController);
            ServiceLocator.Register<SceneLoader>(sceneLoader);

            sceneLoader.LoadScene(SceneName.Gameplay);
        }

        private void OnDestroy() => ServiceLocator.Clear();
    }
}