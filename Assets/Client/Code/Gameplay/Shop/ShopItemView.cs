using System;
using Client.Code.Core.Dispose;
using Client.Code.Core.UI;
using Client.Code.Gameplay.Player.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Client.Code.Gameplay.Shop
{
    public class ShopItemView : MonoBehaviour, IDisposable
    {
        public Image Icon;
        public TextMeshProUGUI Header;
        public TextMeshProUGUI Description;
        public ButtonView BuyButton;
        public Image PriceIcon;
        public TextMeshProUGUI PriceCount;
        private readonly CompositeDisposable _disposable = new();
        private PlayerInventory _playerInventory;

        public bool IsFullyPurchased { get; private set; }
        
        public ShopItemConfig Item { get; private set; }

        public void Construct(PlayerInventory playerInventory, ShopItemConfig item)
        {
            _playerInventory = playerInventory;
            Item = item;
        }

        public void Initialize()
        {
            Icon.sprite = Item.Item.Icon;
            Header.text = Item.Item.Name;
            Description.text = Item.Item.Description;
            PriceIcon.sprite = Item.Price.Item.Icon;
            PriceCount.text = Item.Price.Count.ToString();
            BuyButton.OnClick.Subscribe(TryPurchase).AddTo(_disposable);
        }

        public void Dispose() => _disposable.Dispose();
        
        private void TryPurchase()
        {
            if (_playerInventory.Remove(Item.Price.Item, Item.Price.Count))
            {
                _playerInventory.Add(Item.Item, Item.Count);
                if (!Item.IsInfinite)
                {
                    gameObject.SetActive(false);
                    IsFullyPurchased = true;
                }
            }
        }
    }
}