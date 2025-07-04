using System.Collections.Generic;
using Client.Code.Core.Config;
using Client.Code.Core.Dispose;
using Client.Code.Core.Progress;
using Client.Code.Core.Progress.Actors;
using Client.Code.Core.Settings;
using Client.Code.Core.UI;
using Client.Code.Gameplay.Player.Inventory;
using UnityEngine;

namespace Client.Code.Gameplay.Shop
{
    public class ShopWindow : WindowView, IProgressWriter
    {
        public Transform CurrenciesRoot;
        public Transform OthersRoot;
        public ShopItemView ItemViewPrefab;
        public ButtonView CloseButton;
        private readonly CompositeDisposable _disposables = new();
        private readonly List<ShopItemView> _views = new();
        private IProgressProvider _progressProvider;
        private IConfigsProvider _configsProvider;
        private PlayerInventory _playerInventory;

        public void Construct(IProgressProvider progressProvider, IConfigsProvider configsProvider, PlayerInventory playerInventory)
        {
            _playerInventory = playerInventory;
            _configsProvider = configsProvider;
            _progressProvider = progressProvider;
        }

        public override void Initialize()
        {
            base.Initialize();

            foreach (var item in _configsProvider.Data.ShopItems)
            {
                if (!_progressProvider.Data.Shop.PurchasedItems.Contains(item.Id))
                {
                    var itemView = Instantiate(ItemViewPrefab, item.IsCurrency ? CurrenciesRoot : OthersRoot);
                    itemView.Construct(_playerInventory, item);
                    itemView.Initialize();
                    itemView.AddTo(_disposables);
                    _views.Add(itemView);
                }
            }

            CloseButton.OnClick.Subscribe(Close).AddTo(_disposables);
        }

        public void Dispose() => _disposables.Dispose();

        public void OnWrite(ProgressData progress)
        {
            foreach (var itemView in _views)
                if (itemView.IsFullyPurchased)
                    progress.Shop.PurchasedItems.Add(itemView.Item.Id);
        }
    }
}