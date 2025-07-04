using System;
using Client.Code.Core.Dispose;
using Client.Code.Core.UI;
using Client.Code.Gameplay.Player.Wallet;
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
        private ShopItemController _controller;
        
        public void Initialize(ShopItemController controller, CurrencyProvider currencyProvider)
        {
            _controller = controller;
            Icon.sprite = _controller.Icon;
            Header.text = _controller.Name;
            Description.text = _controller.Description;
            PriceIcon.sprite = currencyProvider.GetConfig(_controller.Price.Type).Icon;
            PriceCount.text = _controller.Price.Count.ToString();
            _controller.IsPurchasedChanged.Subscribe(UpdateView).AddTo(_disposable);
            BuyButton.OnClick.Subscribe(() => controller.TryPurchase()).AddTo(_disposable);
        }

        private void UpdateView()
        {
            if (_controller.IsPurchased)
                gameObject.SetActive(false);
        }

        public void Dispose() => _disposable.Dispose();
    }
}