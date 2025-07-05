using System;

namespace Client.Code.Gameplay.Currency
{
    [Serializable]
    public struct CurrencyAmount
    {
        public CurrencyConfig Item;
        public int Count;
    }
}