using UnityEngine;

namespace Client.Code.Gameplay.Player.Wallet
{
    public readonly struct CurrencyItem
    {
        public readonly CurrencyType Type;
        public readonly int Count;
        private readonly CurrencyConfig _config;

        public Sprite Icon => _config.Icon;
        
        public CurrencyItem(CurrencyType type, int count, CurrencyConfig config)
        {
            Type = type;
            Count = count;
            _config = config;
        }

        public CurrencyItemProgressData ToProgress() => new(Type, Count);
    }
}