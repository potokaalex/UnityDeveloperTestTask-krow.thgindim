namespace Client.Code.Gameplay.Player
{
    public struct InventoryItem
    {
        public InventoryItemType Type;
        public int Count;

        public InventoryItem(InventoryItemType type, int count)
        {
            Type = type;
            Count = count;
        }
    }
}