using Client.Code.Core;
using Client.Code.Core.Dispose;
using Client.Code.Core.Scene;
using Client.Code.Core.ServiceLocatorCode;
using Client.Code.Core.UI;
using UnityEngine;

namespace Client.Code.MainMenu
{
    public class MainMenuInstaller : Context
    {
        public ButtonView GameplayButton;
        public ButtonView SettingsButton;
        public ButtonView ExitButton;
        private readonly CompositeDisposable _disposables = new();

        protected override void Install()
        {
            GameplayButton.OnClick.Subscribe(() => Locator.Get<SceneLoader>().Load(SceneName.Gameplay)).AddTo(_disposables);
            SettingsButton.OnClick.Subscribe(() => Debug.Log("OpenSettings")).AddTo(_disposables);
            ExitButton.OnClick.Subscribe(Exit).AddTo(_disposables);
        }

        protected override void UnInstall() => _disposables.Dispose();

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