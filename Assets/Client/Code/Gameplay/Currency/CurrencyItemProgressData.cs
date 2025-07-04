﻿using System;

namespace Client.Code.Gameplay.Currency
{
    [Serializable]
    public struct CurrencyItemProgressData
    {
        public CurrencyType Type;
        public int Count;

        public CurrencyItemProgressData(CurrencyType type, int count)
        {
            Type = type;
            Count = count;
        }
    }
}