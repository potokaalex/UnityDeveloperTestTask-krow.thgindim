using System;

namespace Client.Code.Gameplay.Player.Inventory
{
    [Serializable]
    public struct InventoryItemProgressData
    {
        public string ItemId;
        public int Count;
        public int CellIndex;

        public InventoryItemProgressData(string itemId, int count, int cellIndex)
        {
            ItemId = itemId;
            Count = count;
            CellIndex = cellIndex;
        }
    }
}