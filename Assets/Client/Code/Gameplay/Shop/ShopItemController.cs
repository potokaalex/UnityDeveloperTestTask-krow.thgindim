using Client.Code.Core.Progress;
using Client.Code.Core.Rx;
using Client.Code.Gameplay.Item;
using Client.Code.Gameplay.Player;
using Client.Code.Gameplay.Player.Inventory;
using UnityEngine;

namespace Client.Code.Gameplay.Shop
{
    public class ShopItemController
    {
        private readonly ShopItemConfig _config;
        private readonly PlayerInventory _playerInventory;
        private readonly IProgressProvider _progressProvider;
        private readonly PlayerScore _playerScore;

        public ShopItemController(ShopItemConfig config, PlayerInventory playerInventory, IProgressProvider progressProvider, PlayerScore playerScore)
        {
            _config = config;
            _playerInventory = playerInventory;
            _progressProvider = progressProvider;
            _playerScore = playerScore;
        }

        public bool IsPurchased { get; private set; }

        public EventAction IsPurchasedChanged { get; } = new();

        public string Id => _config.Id;

        public bool IsCurrency => _config.IsCurrency;

        public Sprite Icon => _config.ToInventory.Item.Icon;

        public string Name => _config.ToInventory.Item.Name;

        public string Description => _config.Description;

        public ItemCount Price => _config.Price;

        public void Initialize()
        {
            if (_progressProvider.Data.Shop.PurchasedItems.Contains(Id))
                IsPurchased = true;
        }

        public bool TryPurchase()
        {
            if (IsPurchased)
                return false;

            if (_playerInventory.Remove(_config.Price.Item, _config.Price.Count))
            {
                if (_playerInventory.Add(_config.ToInventory.Item, _config.ToInventory.Count))
                {
                    _playerScore.Add(1);

                    if (!_config.IsInfinite)
                    {
                        IsPurchased = true;
                        IsPurchasedChanged.Invoke();
                    }

                    return true;
                }
            }

            return false;
        }
    }
}