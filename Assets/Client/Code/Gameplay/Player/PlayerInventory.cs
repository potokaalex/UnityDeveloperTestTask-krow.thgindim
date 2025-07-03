using System.Collections.Generic;
using Client.Code.Core.Progress;
using Client.Code.Core.Progress.Actors;
using Client.Code.Core.Rx;

namespace Client.Code.Gameplay.Player
{
    public class PlayerInventory : IProgressWriter
    {
        private readonly Dictionary<InventoryItemType, InventoryItem> _items = new();
        private readonly IProgressProvider _progressProvider;

        public PlayerInventory(IProgressProvider progressProvider) => _progressProvider = progressProvider;

        public EventAction OnChanged { get; } = new();
        
        public void Initialize()
        {
            foreach (var item in _progressProvider.Data.Player.InventoryItems) 
                _items.Add(item.Type, item);
        }
        
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

        public void OnWrite(ProgressData progress)
        {
            progress.Player.InventoryItems.Clear();
            foreach (var item in _items) 
                progress.Player.InventoryItems.Add(item.Value);
        }
    }
}