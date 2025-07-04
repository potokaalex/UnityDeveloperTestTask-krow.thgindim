using System.Collections.Generic;
using Client.Code.Core.Progress;
using Client.Code.Core.Progress.Actors;
using Client.Code.Core.Rx;

namespace Client.Code.Gameplay.Player.Wallet
{
    public class PlayerWallet : IProgressWriter
    {
        private readonly Dictionary<CurrencyType, CurrencyItem> _items = new();
        private readonly IProgressProvider _progressProvider;
        private readonly CurrencyProvider _currencyProvider;

        public PlayerWallet(IProgressProvider progressProvider, CurrencyProvider currencyProvider)
        {
            _progressProvider = progressProvider;
            _currencyProvider = currencyProvider;
        }

        public EventAction OnChanged { get; } = new();

        public void Initialize()
        {
            foreach (var item in _progressProvider.Data.Player.WalletItems)
                _items.Add(item.Type, new CurrencyItem(item.Type, item.Count, _currencyProvider.GetConfig(item.Type)));
        }

        public void Add(CurrencyAmount item) =>
            Set(new CurrencyItem(item.Type, Get(item.Type).Count + item.Count, _currencyProvider.GetConfig(item.Type)));

        public bool Remove(CurrencyAmount item)
        {
            var inWallet = Get(item.Type);
            if (inWallet.Count < item.Count)
                return false;

            Set(new CurrencyItem(item.Type, inWallet.Count - item.Count, _currencyProvider.GetConfig(item.Type)));
            return true;
        }

        public CurrencyItem Get(CurrencyType type)
        {
            foreach (var item in _items)
                if (item.Key == type)
                    return item.Value;

            return new CurrencyItem(type, 0, _currencyProvider.GetConfig(type));
        }

        public void OnWrite(ProgressData progress)
        {
            progress.Player.WalletItems.Clear();
            foreach (var item in _items)
                progress.Player.WalletItems.Add(item.Value.ToProgress());
        }


        private void Set(CurrencyItem item)
        {
            _items[item.Type] = item;
            OnChanged.Invoke();
        }
    }
}