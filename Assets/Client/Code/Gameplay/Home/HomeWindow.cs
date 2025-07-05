using Client.Code.Core.Dispose;
using Client.Code.Core.Settings;
using Client.Code.Core.UI;
using Client.Code.Gameplay.Player.Inventory;
using Client.Code.Gameplay.Player.Level;
using Client.Code.Gameplay.Player.Wallet;
using Client.Code.Gameplay.Shop;
using UnityEngine;

namespace Client.Code.Gameplay.Home
{
    public class HomeWindow : MonoBehaviour
    {
        public ButtonView OpenBuildingButton;
        public ButtonView OpenShopButton;
        public ButtonView OpenInventoryButton;
        public ButtonView OpenSettingsButton;
        public ButtonView LoadMainMenuButton;
        public PlayerLevelView LevelView;
        public PlayerWalletView PlayerWalletView;
        private readonly CompositeDisposable _disposable = new();
        private GameplayManager _gameplayManager;
        private SettingsWindow _settingsWindow;
        private ShopWindow _shopWindow;
        private InventoryWindow _inventoryWindow;

        public void Construct(GameplayManager gameplayManager, SettingsWindow settingsWindow, ShopWindow shopWindow, InventoryWindow inventoryWindow,
            PlayerLevel playerLevel, PlayerWallet playerWallet)
        {
            _inventoryWindow = inventoryWindow;
            _gameplayManager = gameplayManager;
            _settingsWindow = settingsWindow;
            _shopWindow = shopWindow;
            LevelView.Construct(playerLevel);
            PlayerWalletView.Construct(playerWallet);
        }

        public void Initialize()
        {
            OpenBuildingButton.OnClick.Subscribe(() => Debug.Log("Open building")).AddTo(_disposable);
            LoadMainMenuButton.OnClick.Subscribe(_gameplayManager.LoadMainMenu).AddTo(_disposable);
            OpenSettingsButton.OnClick.Subscribe(_settingsWindow.Open).AddTo(_disposable);
            OpenShopButton.OnClick.Subscribe(_shopWindow.Open).AddTo(_disposable);
            OpenInventoryButton.OnClick.Subscribe(_inventoryWindow.Open).AddTo(_disposable);
            LevelView.Initialize();
            LevelView.AddTo(_disposable);
            PlayerWalletView.Initialize();
            PlayerWalletView.AddTo(_disposable);
        }

        public void Dispose() => _disposable.Dispose();
    }
}