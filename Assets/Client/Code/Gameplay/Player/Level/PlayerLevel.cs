using Client.Code.Core.Progress;
using Client.Code.Core.Progress.Actors;
using Client.Code.Core.Rx;
using Client.Code.Gameplay.Currency;
using UnityEngine;

namespace Client.Code.Gameplay.Player.Level
{
    public class PlayerLevel : IProgressWriter
    {
        private const float FirstLevelProgress = 5;
        private readonly IProgressProvider _progressProvider;
        private readonly CurrencyFactory _currencyFactory;
        private readonly PlayerWallet _playerWallet;

        public PlayerLevel(IProgressProvider progressProvider, CurrencyFactory currencyFactory, PlayerWallet playerWallet)
        {
            _progressProvider = progressProvider;
            _currencyFactory = currencyFactory;
            _playerWallet = playerWallet;
        }

        public float Progress { get; private set; }

        public float MaxProgress => FirstLevelProgress * Mathf.Pow(2, Current - 1);

        public int Current { get; private set; }

        public int Next => Current + 1;

        public EventAction OnChanged { get; } = new();
        
        public EventAction OnLevelChanged { get; } = new();

        public CurrencyAmount LevelUpReward => _currencyFactory.CreateAmount(CurrencyType.Gem, 1);

        public void Initialize()
        {
            Progress = _progressProvider.Data.Player.Level.Progress;
            Current = _progressProvider.Data.Player.Level.Current;
        }

        public void AddProgress(float value)
        {
            Progress += value;
            if (Progress >= MaxProgress)
            {
                Progress = 0;
                Current++;
                _playerWallet.Add(LevelUpReward);
                OnLevelChanged.Invoke();
            }

            OnChanged.Invoke();
        }

        public void OnWrite(ProgressData progress)
        {
            progress.Player.Level.Current = Current;
            progress.Player.Level.Progress = Progress;
        }
    }
}