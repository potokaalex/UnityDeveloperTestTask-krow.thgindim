using Client.Code.Core.Progress;
using Client.Code.Core.Rx;
using Client.Code.Gameplay.Building;
using Client.Code.Gameplay.Currency;
using Client.Code.Gameplay.Player;
using Client.Code.Gameplay.Player.Wallet;
using Client.Code.Gameplay.Restaurant;
using UnityEngine;

namespace Client.Code.Gameplay.CustomerZone
{
    public class CustomerReceptionTable : MonoBehaviour, IBuildingController
    {
        public CurrencyAmount BuildPrice;
        public Transform CustomerPoint;
        public float ServiceTime;
        private IProgressProvider _progressProvider;
        private PlayerWallet _playerWallet;
        private PlayerScore _playerScore;

        public bool IsBuilt { get; private set; }

        CurrencyAmount IBuildingController.BuildPrice => BuildPrice;

        public EventAction OnChanged { get; } = new();

        public void Construct(IProgressProvider progressProvider, PlayerWallet playerWallet, PlayerScore playerScore)
        {
            _playerScore = playerScore;
            _playerWallet = playerWallet;
            _progressProvider = progressProvider;
        }

        public void Initialize()
        {
            IsBuilt = _progressProvider.Data.Restaurant.HasReception;
            gameObject.SetActive(IsBuilt);
        }

        public void TryBuild()
        {
            if (_playerWallet.Remove(BuildPrice))
            {
                IsBuilt = true;
                _playerScore.Add(3);
                OnChanged.Invoke();
                gameObject.SetActive(true);
            }
        }

        public void WriteProgress(RestaurantProgressData progress) => progress.HasReception = IsBuilt;
    }
}