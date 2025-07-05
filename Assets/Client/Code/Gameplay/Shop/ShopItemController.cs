using Client.Code.Core.Progress;
using Client.Code.Core.Rx;
using Client.Code.Gameplay.Currency;
using Client.Code.Gameplay.Player;
using Client.Code.Gameplay.Player.Inventory;
using UnityEngine;

namespace Client.Code.Gameplay.Shop
{
    public class ShopItemController
    {
        private readonly ShopItemConfig _config;
        private readonly PlayerInventory _playerInventory;
        private readonly PlayerWallet _playerWallet;
        private readonly IProgressProvider _progressProvider;
        private readonly PlayerScore _playerScore;

        public ShopItemController(ShopItemConfig config, PlayerInventory playerInventory, PlayerWallet playerWallet,
            IProgressProvider progressProvider, PlayerScore playerScore)
        {
            _config = config;
            _playerInventory = playerInventory;
            _playerWallet = playerWallet;
            _progressProvider = progressProvider;
            _playerScore = playerScore;
        }

        public bool IsPurchased { get; private set; }

        public EventAction IsPurchasedChanged { get; } = new();

        public string Id => _config.Id;

        public bool IsCurrency => _config.IsCurrency;

        public Sprite Icon => _config.IsCurrency ? _config.ToWallet.Config.Icon : _config.ToInventory.Config.Icon;

        public string Name => _config.Name;

        public string Description => _config.Description;

        public CurrencyAmount Price => _config.Price;

        public void Initialize()
        {
            if (_progressProvider.Data.Shop.PurchasedItems.Contains(Id))
                IsPurchased = true;
        }

        public bool TryPurchase()
        {
            if (IsPurchased)
                return false;

            if (_playerWallet.Remove(_config.Price))
            {
                if (_config.IsCurrency)
                {
                    _playerWallet.Add(_config.ToWallet);
                    _playerScore.Add(1);
                    return true;
                }

                if (_playerInventory.Add(_config.ToInventory))
                {
                    IsPurchased = true;
                    IsPurchasedChanged.Invoke();
                    _playerScore.Add(1);
                    return true;
                }

                return false;
            }

            return false;
        }
    }
}