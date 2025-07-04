using Client.Code.Core.Audio;
using Client.Code.Core.Dispose;
using Client.Code.Core.Scene;
using Client.Code.Core.ServiceLocatorCode;
using Client.Code.Core.Settings;
using Client.Code.Core.UI;

namespace Client.Code.MainMenu
{
    public class MainMenuInstaller : Context
    {
        public ButtonView GameplayButton;
        public ButtonView SettingsButton;
        public ButtonView ExitButton;
        public SettingsWindow SettingsWindow;
        private readonly CompositeDisposable _disposables = new();

        protected override void Install()
        {
            //create
            SettingsWindow.Construct(Locator.Get<AudioController>());
            
            //init
            SettingsWindow.Initialize();
            GameplayButton.OnClick.Subscribe(() => Locator.Get<SceneLoader>().Load(SceneName.Gameplay)).AddTo(_disposables);
            SettingsButton.OnClick.Subscribe(() => SettingsWindow.Open()).AddTo(_disposables);
            ExitButton.OnClick.Subscribe(Exit).AddTo(_disposables);
        }

        protected override void UnInstall()
        {
            _disposables.Dispose();
            SettingsWindow.Dispose();
        }

        private void Exit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}