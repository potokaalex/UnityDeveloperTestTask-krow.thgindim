using System;
using Client.Code.Core.Dispose;
using Client.Code.Core.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Client.Code.Gameplay.Player.Inventory
{
    public class InventoryCellView : MonoBehaviour, IDisposable
    {
        public Image Background;
        public Image Icon;
        public ButtonView Button;
        public Color DefaultColor;
        public Color SelectedColor;
        public TextMeshProUGUI CountText;
        private readonly CompositeDisposable _disposables = new();
        private InventoryWindow _inventoryWindow;

        public int CellIndex { get; private set; }

        public void Initialize(InventoryWindow inventoryWindow, int cellIndex)
        {
            CellIndex = cellIndex;
            _inventoryWindow = inventoryWindow;
            Clear();
            Button.OnClick.Subscribe(OnClick).AddTo(_disposables);
        }

        public void Dispose() => _disposables.Dispose();

        public void View(InventoryItem item)
        {
            Icon.sprite = item.Config.Icon;
            Icon.color = Color.white;
            CountText.SetText(item.Count.ToString());
        }

        public void Clear()
        {
            Icon.color = Color.clear;
            CountText.SetText("");
        }

        public void Select() => Background.color = SelectedColor;

        public void UnSelect() => Background.color = DefaultColor;

        private void OnClick() => _inventoryWindow.OnSelection(this);
    }
}