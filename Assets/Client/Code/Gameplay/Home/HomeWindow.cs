using Client.Code.Core.Dispose;
using Client.Code.Core.Scene;
using Client.Code.Core.UI;
using Client.Code.Gameplay.CustomerZone;
using Client.Code.Gameplay.Player;
using Client.Code.Gameplay.Restaurant;
using TMPro;
using UnityEngine;

namespace Client.Code.Gameplay.Home
{
    public class HomeWindow : MonoBehaviour
    {
        public TextMeshProUGUI Score;
        public TextMeshProUGUI Gold;
        public BuyPanel BuyCustomerTable;
        public ButtonView LoadMainMenuButton;
        private readonly CompositeDisposable _disposable = new();
        private PlayerInventory _playerInventory;
        private CustomerZoneController _customerZoneController;
        private PlayerScore _playerScore;
        private GameplayManager _gameplayManager;


        public void Construct(PlayerInventory playerInventory, CustomerZoneController customerZoneController, PlayerScore playerScore, GameplayManager gameplayManager)
        {
            _gameplayManager = gameplayManager;
            _playerScore = playerScore;
            _customerZoneController = customerZoneController;
            _playerInventory = playerInventory;
        }

        public void Initialize()
        {
            _playerInventory.OnChanged.Subscribe(UpdateView).AddTo(_disposable);
            _customerZoneController.OnTableBuild.Subscribe(UpdateView).AddTo(_disposable);
            _playerScore.OnScoreChanged.Subscribe(UpdateView).AddTo(_disposable);
            BuyCustomerTable.BuyButton.OnClick.Subscribe(() => _customerZoneController.BuildTable()).AddTo(_disposable);
            LoadMainMenuButton.OnClick.Subscribe(_gameplayManager.LoadMainMenu).AddTo(_disposable);
            UpdateView();
        }

        public void Dispose() => _disposable.Dispose();

        private void UpdateView()
        {
            Score.SetText($"Score: {_playerScore.Score}");
            
            var gold = _playerInventory.Get(InventoryItemType.Gold);
            Gold.SetText($"Gold: {gold.Count}");
         
            BuyCustomerTable.Description.SetText(
                $"CustomerTables: {_customerZoneController.TablesAliveCount}/{_customerZoneController.TablesMaxCount}");
            BuyCustomerTable.Price.SetText(_customerZoneController.TableBuildPrice.Count.ToString());
        }
    }
}