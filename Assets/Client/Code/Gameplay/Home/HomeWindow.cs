using Client.Code.Core.Dispose;
using Client.Code.Core.Settings;
using Client.Code.Core.UI;
using Client.Code.Gameplay.Currency;
using Client.Code.Gameplay.CustomerZone;
using Client.Code.Gameplay.Player;
using Client.Code.Gameplay.Player.Inventory;
using Client.Code.Gameplay.Shop;
using TMPro;
using UnityEngine;

namespace Client.Code.Gameplay.Home
{
    public class HomeWindow : MonoBehaviour
    {
        public TextMeshProUGUI Score;
        public TextMeshProUGUI Cash;
        public TextMeshProUGUI Gem;
        public BuyPanel BuyCustomerTable;
        public ButtonView LoadMainMenuButton;
        public ButtonView OpenSettingsButton;
        public ButtonView OpenShopButton;
        public ButtonView OpenInventoryButton;
        private readonly CompositeDisposable _disposable = new();
        private CustomerZoneController _customerZoneController;
        private PlayerScore _playerScore;
        private GameplayManager _gameplayManager;
        private SettingsWindow _settingsWindow;
        private ShopWindow _shopWindow;
        private PlayerWallet _playerWallet;
        private InventoryWindow _inventoryWindow;

        public void Construct(CustomerZoneController customerZoneController, PlayerScore playerScore, GameplayManager gameplayManager,
            SettingsWindow settingsWindow, ShopWindow shopWindow, PlayerWallet playerWallet, InventoryWindow inventoryWindow)
        {
            _inventoryWindow = inventoryWindow;
            _playerWallet = playerWallet;
            _gameplayManager = gameplayManager;
            _playerScore = playerScore;
            _customerZoneController = customerZoneController;
            _settingsWindow = settingsWindow;
            _shopWindow = shopWindow;
        }

        public void Initialize()
        {
            _playerWallet.OnChanged.Subscribe(UpdateView).AddTo(_disposable);
            _customerZoneController.OnTableBuild.Subscribe(UpdateView).AddTo(_disposable);
            _playerScore.OnScoreChanged.Subscribe(UpdateView).AddTo(_disposable);
            BuyCustomerTable.BuyButton.OnClick.Subscribe(() => _customerZoneController.BuildTable()).AddTo(_disposable);
            LoadMainMenuButton.OnClick.Subscribe(_gameplayManager.LoadMainMenu).AddTo(_disposable);
            UpdateView();
            OpenSettingsButton.OnClick.Subscribe(_settingsWindow.Open).AddTo(_disposable);
            OpenShopButton.OnClick.Subscribe(_shopWindow.Open).AddTo(_disposable);
            OpenInventoryButton.OnClick.Subscribe(_inventoryWindow.Open).AddTo(_disposable);
        }

        public void Dispose() => _disposable.Dispose();

        private void UpdateView()
        {
            Score.SetText($"Score: {_playerScore.Score}");

            Cash.SetText($"Cash: {_playerWallet.Get(CurrencyType.Cash).Count}");
            Gem.SetText($"Gem: {_playerWallet.Get(CurrencyType.Gem).Count}");

            BuyCustomerTable.Description.SetText(
                $"CustomerTables: {_customerZoneController.TablesAliveCount}/{_customerZoneController.TablesMaxCount}");
            BuyCustomerTable.Price.SetText(_customerZoneController.TableBuildPrice.Count.ToString());
        }
    }
}