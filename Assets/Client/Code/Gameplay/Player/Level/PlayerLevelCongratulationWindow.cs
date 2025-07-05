using Client.Code.Core.Dispose;
using Client.Code.Core.Settings;
using Client.Code.Core.UI;
using UnityEngine.UI;

namespace Client.Code.Gameplay.Player.Level
{
    public class PlayerLevelCongratulationWindow : WindowView
    {
        public ButtonView CloseButton;
        public Image Icon;
        private PlayerLevel _playerLevel;
        private readonly CompositeDisposable _disposables = new();

        public void Construct(PlayerLevel playerLevel) => _playerLevel = playerLevel;

        public override void Initialize()
        {
            base.Initialize();
            _playerLevel.OnLevelChanged.Subscribe(Open).AddTo(_disposables);
            CloseButton.OnClick.Subscribe(Close).AddTo(_disposables);
            Icon.sprite = _playerLevel.LevelUpReward.Config.Icon;
        }

        public void Dispose() => _disposables.Clear();
    }
}