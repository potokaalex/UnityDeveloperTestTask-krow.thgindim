using System.Collections.Generic;
using Client.Code.Core.Progress;
using Client.Code.Core.Progress.Actors;
using Client.Code.Core.Rx;
using Client.Code.Gameplay.Item;

namespace Client.Code.Gameplay.Player.Inventory
{
    public class PlayerInventory : IProgressWriter
    {
        private readonly IProgressProvider _progressProvider;
        private readonly ItemsProvider _itemsProvider;
        private List<InventoryItem> _items;

        public PlayerInventory(IProgressProvider progressProvider, ItemsProvider itemsProvider)
        {
            _itemsProvider = itemsProvider;
            _progressProvider = progressProvider;
        }

        public EventAction OnChanged { get; } = new();

        public int CellsCount => 10;

        public void Initialize()
        {
            _items = new(CellsCount);
            foreach (var progress in _progressProvider.Data.Player.InventoryItems)
                _items.Add(new InventoryItem(_itemsProvider.Get(progress.ItemId), progress.Count, progress.CellIndex));
        }

        public bool Add(ItemConfig item, int count)
        {
            if (count <= 0)
                return false;
            
            if (TryGetItem(item, out var itemIndex))
                Set(itemIndex, new InventoryItem(item, count, _items[itemIndex].CellIndex));

            if (TryGetEmptyCell(out var cellIndex))
            {
                _items.Add(default);
                Set(_items.Count - 1, new InventoryItem(item, count, cellIndex));
                return true;
            }

            return false;
        }

        public bool Remove(ItemConfig item, int count)
        {
            if (count <= 0)
                return true;
            
            if (TryGetItem(item, out var itemIndex))
            {
                var i = _items[itemIndex];
                if (i.Count >= count)
                {
                    Set(itemIndex, new InventoryItem(item, i.Count - count, i.CellIndex));
                    return true;
                }

                return false;
            }

            return false;
        }

        public InventoryItem Get(ItemConfig item)
        {
            TryGetItem(item, out var index);
            return _items[index];
        }

        public int GetCount(ItemConfig item)
        {
            if (TryGetItem(item, out var index))
                return _items[index].Count;
            return 0;
        }

        public void OnWrite(ProgressData progress)
        {
            progress.Player.InventoryItems.Clear();
            foreach (var item in _items)
                progress.Player.InventoryItems.Add(new InventoryItemProgressData(item.Config.Id, item.Count, item.CellIndex));
        }

        private void Set(int index, InventoryItem item)
        {
            _items[index] = item;
            OnChanged.Invoke();
        }

        private bool TryGetItem(ItemConfig config, out int itemIndex)
        {
            for (var i = 0; i < _items.Count; i++)
            {
                if (_items[i].Config == config)
                {
                    itemIndex = i;
                    return true;
                }
            }

            itemIndex = -1;
            return false;
        }

        private bool TryGetEmptyCell(out int cellIndex)
        {
            for (var c = 0; c < CellsCount; c++)
            {
                if (IsCellEmpty(c))
                {
                    cellIndex = c;
                    return true;
                }
            }

            cellIndex = -1;
            return false;
        }

        private bool IsCellEmpty(int cellIndex)
        {
            for (var i = 0; i < _items.Count; i++)
                if (_items[i].CellIndex == cellIndex)
                    return false;
            return true;
        }
    }
}