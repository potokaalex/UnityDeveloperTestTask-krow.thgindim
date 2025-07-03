using System.Collections.Generic;
using Client.Code.Core;

namespace Client.Code.Gameplay.Player
{
    public class PlayerInventory
    {
        private readonly Dictionary<InventoryItemType, InventoryItem> _items = new();

        public EventAction OnChanged { get; } = new();

        public void Add(InventoryItem item) => Set(new InventoryItem(item.Type, Get(item.Type).Count + item.Count));

        public void Remove(InventoryItem item) => Set(new InventoryItem(item.Type, Get(item.Type).Count - item.Count));

        public InventoryItem Get(InventoryItemType type)
        {
            if (_items.TryGetValue(type, out var item))
                return item;
            return new(type, 0);
        }

        private void Set(InventoryItem value)
        {
            _items[value.Type] = value;
            OnChanged.Invoke();
        }
    }
}