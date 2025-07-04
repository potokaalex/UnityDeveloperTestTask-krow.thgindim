using Client.Code.Core.Progress;
using Client.Code.Core.Rx;
using Client.Code.Gameplay.Player;
using Client.Code.Gameplay.Player.Inventory;
using Client.Code.Gameplay.Player.Wallet;
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
        private readonly CurrencyProvider _currencyProvider;

        public ShopItemController(ShopItemConfig config, PlayerInventory playerInventory, PlayerWallet playerWallet,
            IProgressProvider progressProvider, PlayerScore playerScore, CurrencyProvider currencyProvider)
        {
            _config = config;
            _playerInventory = playerInventory;
            _playerWallet = playerWallet;
            _progressProvider = progressProvider;
            _playerScore = playerScore;
            _currencyProvider = currencyProvider;
        }

        public bool IsPurchased { get; private set; }

        public EventAction IsPurchasedChanged { get; } = new();

        public string Id => _config.Id;

        public bool IsCurrency => _config.IsCurrency;

        public Sprite Icon => _config.IsCurrency ? _currencyProvider.GetConfig(_config.ToWallet.Type).Icon : _config.ToInventory.Item.Icon;

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