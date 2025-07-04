using Client.Code.Core.Dispose;
using Client.Code.Core.Settings;
using Client.Code.Core.UI;
using Client.Code.Gameplay.CustomerZone;
using Client.Code.Gameplay.Item;
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
        private readonly CompositeDisposable _disposable = new();
        private CustomerZoneController _customerZoneController;
        private PlayerScore _playerScore;
        private GameplayManager _gameplayManager;
        private SettingsWindow _settingsWindow;
        private PlayerInventory _playerInventory;
        private ItemsProvider _itemsProvider;
        private ShopWindow _shopWindow;

        public void Construct(PlayerInventory playerInventory, CustomerZoneController customerZoneController, PlayerScore playerScore,
            GameplayManager gameplayManager, SettingsWindow settingsWindow, ItemsProvider itemsProvider, ShopWindow shopWindow)
        {
            _itemsProvider = itemsProvider;
            _playerInventory = playerInventory;
            _gameplayManager = gameplayManager;
            _playerScore = playerScore;
            _customerZoneController = customerZoneController;
            _settingsWindow = settingsWindow;
            _shopWindow = shopWindow;
        }

        public void Initialize()
        {
            _playerInventory.OnChanged.Subscribe(UpdateView).AddTo(_disposable);
            _customerZoneController.OnTableBuild.Subscribe(UpdateView).AddTo(_disposable);
            _playerScore.OnScoreChanged.Subscribe(UpdateView).AddTo(_disposable);
            BuyCustomerTable.BuyButton.OnClick.Subscribe(() => _customerZoneController.BuildTable()).AddTo(_disposable);
            LoadMainMenuButton.OnClick.Subscribe(_gameplayManager.LoadMainMenu).AddTo(_disposable);
            UpdateView();
            OpenSettingsButton.OnClick.Subscribe(_settingsWindow.Open).AddTo(_disposable);
            OpenShopButton.OnClick.Subscribe(_shopWindow.Open).AddTo(_disposable);
        }

        public void Dispose() => _disposable.Dispose();

        private void UpdateView()
        {
            Score.SetText($"Score: {_playerScore.Score}");

            Cash.SetText($"Cash: {_playerInventory.GetCount(_itemsProvider.Get("cash"))}");

            Gem.SetText($"Gem: {_playerInventory.GetCount(_itemsProvider.Get("gem"))}");

            BuyCustomerTable.Description.SetText(
                $"CustomerTables: {_customerZoneController.TablesAliveCount}/{_customerZoneController.TablesMaxCount}");
            BuyCustomerTable.Price.SetText(_customerZoneController.TableBuildPrice.Count.ToString());
        }
    }
}