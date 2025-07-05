using System;
using UnityEngine.Serialization;

namespace Client.Code.Gameplay.Currency
{
    [Serializable]
    public struct CurrencyAmount
    {
        [FormerlySerializedAs("Item")] public CurrencyConfig Config;
        public int Count;
    }
}