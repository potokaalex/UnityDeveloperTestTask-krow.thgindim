using System;
using Client.Code.Core.Dispose;
using Client.Code.Gameplay.Currency;
using UnityEngine;

namespace Client.Code.Gameplay.Player.Wallet
{
    public class PlayerWalletView : MonoBehaviour, IDisposable
    {
        public CurrencyView Cash;
        public CurrencyView Gem;
        private PlayerWallet _playerWallet;
        private readonly CompositeDisposable _disposables = new();

        public void Construct(PlayerWallet playerWallet) => _playerWallet = playerWallet;

        public void Initialize()
        {
            _playerWallet.OnChanged.Subscribe(UpdateView).AddTo(_disposables);
            UpdateView();
        }

        public void Dispose() => _disposables.Dispose();

        private void UpdateView()
        {
            Cash.View(_playerWallet.Get(CurrencyType.Cash));
            Gem.View(_playerWallet.Get(CurrencyType.Gem));
        }
    }
}