using Client.Code.Gameplay.Item;

namespace Client.Code.Gameplay.Player.Inventory
{
    public struct InventoryItem
    {
        public ItemConfig Config;
        public int Count;
        public int CellIndex;

        public InventoryItem(ItemConfig config, int count, int cellIndex)
        {
            Config = config;
            Count = count;
            CellIndex = cellIndex;
        }
    }
}