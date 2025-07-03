using System;
using System.Collections.Generic;

namespace Client.Code.Gameplay.Player
{
    [Serializable]
    public class PlayerProgressData
    {
        public List<InventoryItem> InventoryItems = new();
    }
}