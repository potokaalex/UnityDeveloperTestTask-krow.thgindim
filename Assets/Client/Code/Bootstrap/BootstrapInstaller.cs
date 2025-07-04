using Client.Code.Core;
using Client.Code.Core.Audio;
using Client.Code.Core.Config;
using Client.Code.Core.Progress;
using Client.Code.Core.Scene;
using Client.Code.Core.ServiceLocatorCode;

namespace Client.Code.Bootstrap
{
    public class BootstrapInstaller : ProjectContext
    {
        public LoadingScreen LoadingScreen;
        public AudioController AudioController;
        private ProgressController _progressController;

        protected override void Install()
        {
            DontDestroyOnLoad(this);
            
            //create
            var configsController = new ConfigsController();
            _progressController = new ProgressController();
            var coroutineRunner = new CoroutineRunner(this);
            var sceneLoader = new SceneLoader(configsController, coroutineRunner, LoadingScreen);
            AudioController.Construct(_progressController);

            //bind
            Locator.Register<ProgressController>(_progressController);
            Locator.Register<SceneLoader>(sceneLoader);
            Locator.Register<AudioController>(AudioController);

            //init
            configsController.Initialize();
            _progressController.Initialize();
            _progressController.RegisterActor(AudioController);
            AudioController.Initialize();
            sceneLoader.Load(SceneName.MainMenu);
        }

        protected override void UnInstall() => _progressController.Save();

        public void OnApplicationFocus(bool hasFocus) => _progressController.OnApplicationFocus(hasFocus);
    }
}