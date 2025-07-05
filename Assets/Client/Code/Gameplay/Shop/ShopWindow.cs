using Client.Code.Core.Dispose;
using Client.Code.Core.Settings;
using Client.Code.Core.UI;
using UnityEngine;

namespace Client.Code.Gameplay.Shop
{
    public class ShopWindow : WindowView
    {
        public Transform CurrenciesRoot;
        public Transform OthersRoot;
        public ShopItemView ItemViewPrefab;
        public ButtonView CloseButton;
        private readonly CompositeDisposable _disposables = new();
        private ShopController _shopController;

        public void Construct(ShopController shopController) => _shopController = shopController;

        public override void Initialize()
        {
            base.Initialize();

            foreach (var controller in _shopController.ItemControllers)
            {
                if (!controller.IsPurchased)
                {
                    var view = Instantiate(ItemViewPrefab, controller.IsCurrency ? CurrenciesRoot : OthersRoot);
                    view.Initialize(controller);
                    view.AddTo(_disposables);
                }
            }

            CloseButton.OnClick.Subscribe(Close).AddTo(_disposables);
        }

        public void Dispose() => _disposables.Dispose();
    }
}