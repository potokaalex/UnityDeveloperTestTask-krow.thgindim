using System;
using System.Collections.Generic;

namespace Client.Code.Gameplay.Shop
{
    [Serializable]
    public class ShopProgressData
    {
        public List<string> PurchasedItems = new();
    }
}