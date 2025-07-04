using Client.Code.Core.Progress;
using Client.Code.Core.Scene;

namespace Client.Code.Gameplay
{
    public class GameplayManager
    {
        private readonly SceneLoader _sceneLoader;
        private readonly ProgressController _progressController;

        public GameplayManager(SceneLoader sceneLoader, ProgressController progressController)
        {
            _sceneLoader = sceneLoader;
            _progressController = progressController;
        }

        public void LoadMainMenu()
        {
            _progressController.Save();
            _sceneLoader.Load(SceneName.MainMenu);
        }
    }
}