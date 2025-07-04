﻿using System;
using System.Collections.Generic;
using Client.Code.Gameplay.Currency;
using Client.Code.Gameplay.Player.Inventory;
using Client.Code.Gameplay.Player.Level;

namespace Client.Code.Gameplay.Player
{
    [Serializable]
    public class PlayerProgressData
    {
        public List<InventoryItemProgressData> InventoryItems = new();
        public List<CurrencyItemProgressData> WalletItems = new();
        public PlayerLevelProgress Level = new();
    }
}