using System;

namespace Client.Code.Gameplay.Player.Wallet
{
    [Serializable]
    public struct CurrencyAmount
    {
        public CurrencyType Type;
        public int Count;
    }
}