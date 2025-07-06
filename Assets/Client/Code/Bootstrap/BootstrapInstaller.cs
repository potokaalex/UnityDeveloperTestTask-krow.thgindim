using Client.Code.Core;
using Client.Code.Core.Audio;
using Client.Code.Core.Config;
using Client.Code.Core.LifeTime.Events;
using Client.Code.Core.Progress;
using Client.Code.Core.Scene;
using Client.Code.Core.ServiceLocatorCode;

namespace Client.Code.Bootstrap
{
    public class BootstrapInstaller : ProjectContext, IInitializable
    {
        public LoadingScreen LoadingScreen;
        public AudioController AudioController;
        private ProgressController _progressController;
        private SceneLoader _sceneLoader;

        protected override void Install()
        {
            //configs
            var configsController = new ConfigsController();
            Locator.Register(configsController, typeof(IConfigsProvider));
            LifeTime.Register(configsController);

            //progress
            _progressController = new ProgressController();
            Locator.Register(_progressController, typeof(ProgressController));
            LifeTime.Register(_progressController);

            var coroutineRunner = new CoroutineRunner(this);

            //scene loader
            _sceneLoader = new SceneLoader(configsController, coroutineRunner, LoadingScreen);
            Locator.Register(_sceneLoader, typeof(SceneLoader));

            //audio
            AudioController.Construct(_progressController);
            Locator.Register(AudioController, typeof(AudioController));
            LifeTime.Register(AudioController);
            _progressController.Register(AudioController);

            LifeTime.Register(this);
        }

        protected override void UnInstall()
        {
            _progressController.Save();
            LifeTime.Dispose();
        }

        public void Initialize() => _sceneLoader.Load(SceneName.MainMenu);
    }
}