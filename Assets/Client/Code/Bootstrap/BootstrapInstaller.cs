using Client.Code.Core.Config;
using Client.Code.Core.Scene;
using UnityEngine;

namespace Client.Code.Bootstrap
{
    public class BootstrapInstaller : MonoBehaviour
    {
        public void Awake()
        {
            DontDestroyOnLoad(this);

            var configsController = new ConfigsController();
            configsController.Initialize();

            var sceneLoader = new SceneLoader(configsController);
            
            sceneLoader.LoadScene(SceneName.Gameplay);
        }
    }
}