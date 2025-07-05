using System.Collections.Generic;
using Client.Code.Core.Dispose;
using Client.Code.Core.Settings;
using Client.Code.Core.UI;
using Client.Code.Gameplay.Craft;
using UnityEngine;
using UnityEngine.Pool;

namespace Client.Code.Gameplay.Player.Inventory
{
    public class InventoryWindow : WindowView
    {
        public ButtonView ExitButton;
        public InventoryCellView CellPrefab;
        public Transform CellRoot;
        public CraftPanel CraftPanel;
        private readonly CompositeDisposable _disposables = new();
        private readonly Dictionary<int, InventoryCellView> _cells = new();
        private PlayerInventory _playerInventory;
        private InventoryCellView _selected;

        public void Construct(PlayerInventory playerInventory, CraftController craftController)
        {
            _playerInventory = playerInventory;
            CraftPanel.Construct(craftController);
        }

        public override void Initialize()
        {
            base.Initialize();
            ExitButton.OnClick.Subscribe(Close).AddTo(_disposables);
            _playerInventory.OnChanged.Subscribe(ViewItems).AddTo(_disposables);
            CraftPanel.Initialize();
            CraftPanel.AddTo(_disposables);
            CreateCells();
            ViewItems();
        }

        public void Dispose() => _disposables.Dispose();

        public void OnSelection(InventoryCellView cell)
        {
            _selected?.UnSelect();

            if (_selected == cell)
                _selected = null;
            else if (!_selected)
            {
                if (_playerInventory.Has(cell.CellIndex))
                {
                    _selected = cell;
                    cell.Select();
                }
            }
            else
            {
                _playerInventory.Move(_selected.CellIndex, cell.CellIndex);
                _selected = null;
            }
        }

        private void ViewItems()
        {
            foreach (var cell in _cells)
                cell.Value.Clear();

            using var d = ListPool<InventoryItem>.Get(out var items);
            _playerInventory.GetAll(items);
            foreach (var item in items)
                _cells[item.CellIndex].View(item);
        }

        private void CreateCells()
        {
            for (var i = 0; i < _playerInventory.CellsCount; i++)
            {
                var cell = Instantiate(CellPrefab, CellRoot);
                cell.Initialize(this, i);
                cell.AddTo(_disposables);
                _cells.Add(i, cell);
            }
        }
    }
}