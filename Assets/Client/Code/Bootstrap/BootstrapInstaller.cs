using Client.Code.Core;
using Client.Code.Core.Config;
using Client.Code.Core.Progress;
using Client.Code.Core.Scene;
using Client.Code.Core.ServiceLocatorCode;

namespace Client.Code.Bootstrap
{
    public class BootstrapInstaller : ProjectContext
    {
        public LoadingScreen LoadingScreen;
        private ProgressController _progressController;

        protected override void Install()
        {
            DontDestroyOnLoad(this);
            
            var configsController = new ConfigsController();
            _progressController = new ProgressController();
            var coroutineRunner = new CoroutineRunner(this);
            var sceneLoader = new SceneLoader(configsController, coroutineRunner, LoadingScreen);
            
            Locator.Register<ProgressController>(_progressController);
            Locator.Register<SceneLoader>(sceneLoader);
            
            configsController.Initialize();
            _progressController.Initialize();
            sceneLoader.Load(SceneName.MainMenu);
        }
        
        public void OnApplicationFocus(bool hasFocus) => _progressController.OnApplicationFocus(hasFocus);
    }
}