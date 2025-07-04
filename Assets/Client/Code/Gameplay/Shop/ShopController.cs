using System.Collections.Generic;
using Client.Code.Core.Config;
using Client.Code.Core.Progress;
using Client.Code.Core.Progress.Actors;
using Client.Code.Gameplay.Player;
using Client.Code.Gameplay.Player.Inventory;
using Client.Code.Gameplay.Player.Wallet;

namespace Client.Code.Gameplay.Shop
{
    public class ShopController : IProgressWriter
    {
        private readonly IConfigsProvider _configsProvider;
        private readonly PlayerInventory _playerInventory;
        private readonly IProgressProvider _progressProvider;
        private readonly PlayerScore _playerScore;
        private readonly PlayerWallet _playerWallet;
        private readonly CurrencyProvider _currencyProvider;

        public ShopController(IConfigsProvider configsProvider, PlayerInventory playerInventory, IProgressProvider progressProvider,
            PlayerScore playerScore, PlayerWallet playerWallet, CurrencyProvider currencyProvider)
        {
            _configsProvider = configsProvider;
            _playerInventory = playerInventory;
            _progressProvider = progressProvider;
            _playerScore = playerScore;
            _playerWallet = playerWallet;
            _currencyProvider = currencyProvider;
        }

        public List<ShopItemController> ItemControllers { get; } = new();

        public void Initialize()
        {
            foreach (var item in _configsProvider.Data.ShopItems)
            {
                var controller = new ShopItemController(item, _playerInventory,_playerWallet, _progressProvider, _playerScore, _currencyProvider);
                controller.Initialize();
                ItemControllers.Add(controller);
            }
        }

        public void OnWrite(ProgressData progress)
        {
            var items = progress.Shop.PurchasedItems;
            items.Clear();

            foreach (var controller in ItemControllers)
                if (controller.IsPurchased)
                    items.Add(controller.Id);
        }
    }
}