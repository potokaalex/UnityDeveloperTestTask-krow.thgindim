﻿using System.Collections.Generic;
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
        private List<InventoryItem> _items; //it is better to make an array.

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

        public bool Add(ItemAmount amount)
        {
            if (amount.Count <= 0)
                return false;

            if (TryGetItem(amount.Config, out var itemIndex))
            {
                Set(itemIndex, new InventoryItem(amount.Config, _items[itemIndex].Count + amount.Count, _items[itemIndex].CellIndex));
                return true;
            }

            if (TryGetEmptyCell(out var cellIndex))
            {
                _items.Add(default);
                Set(_items.Count - 1, new InventoryItem(amount.Config, amount.Count, cellIndex));
                return true;
            }

            return false;
        }

        public bool Remove(ItemAmount amount)
        {
            if (amount.Count <= 0)
                return true;

            if (TryGetItem(amount.Config, out var itemIndex))
            {
                var i = _items[itemIndex];
                if (i.Count >= amount.Count)
                {
                    Set(itemIndex, new InventoryItem(amount.Config, i.Count - amount.Count, i.CellIndex));
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

        public void GetAll(List<InventoryItem> outList)
        {
            outList.Clear();
            foreach (var item in _items)
                if (item.Valid)
                    outList.Add(item);
        }

        public int GetCount(ItemConfig item)
        {
            if (TryGetItem(item, out var index))
                return _items[index].Count;
            return 0;
        }

        public void Move(int fromCell, int toCell)
        {
            var fromItem = new InventoryItem();
            if (TryGetItemInCell(fromCell, out var fromIndex))
                fromItem = _items[fromIndex];

            var toItem = new InventoryItem();
            if (TryGetItemInCell(toCell, out var toIndex))
                toItem = _items[toIndex];

            if (fromItem.Valid)
                _items[fromIndex] = new InventoryItem(fromItem.Config, fromItem.Count, toCell);
            if (toItem.Valid)
                _items[toIndex] = new InventoryItem(toItem.Config, toItem.Count, fromCell);

            if (fromItem.Valid || toItem.Valid)
                OnChanged.Invoke();
        }

        public bool Has(int cellIndex) => TryGetItemInCell(cellIndex, out _);

        public bool Has(ItemAmount amount) => GetCount(amount.Config) >= amount.Count;

        public void OnWrite(ProgressData progress)
        {
            progress.Player.InventoryItems.Clear();
            foreach (var item in _items)
                progress.Player.InventoryItems.Add(new InventoryItemProgressData(item.Config.Id, item.Count, item.CellIndex));
        }

        private void Set(int index, InventoryItem item)
        {
            _items[index] = item;
            if (!item.Valid)
                _items.RemoveAt(index);
            OnChanged.Invoke();
        }

        private bool TryGetItemInCell(int cellIndex, out int itemIndex)
        {
            for (var i = 0; i < _items.Count; i++)
            {
                if (_items[i].Config && _items[i].CellIndex == cellIndex)
                {
                    itemIndex = i;
                    return true;
                }
            }

            itemIndex = -1;
            return false;
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