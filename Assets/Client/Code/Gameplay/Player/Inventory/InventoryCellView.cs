using Client.Code.Core.Dispose;
using Client.Code.Core.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Client.Code.Gameplay.Player.Inventory
{
    public class InventoryCellView : MonoBehaviour
    {
        public Image Background;
        public Image Icon;
        public ButtonView Button;
        public Color DefaultColor;
        public Color SelectedColor;
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
        }

        public void Clear() => 
            Icon.color = Color.clear;

        public void Select() => Background.color = SelectedColor;

        public void UnSelect() => Background.color = DefaultColor;
        
        private void OnClick()
        {
            _inventoryWindow.OnSelection(this);

            //2) перенос.

            //1) перенос айтемов между клетками.
            //при нажатии на клетку, айтем типа выделяется, если нажмём на него повторно, то выделение снимется.
            //если нажмём на свободную клетку, то айтем переместиться в неё.
        }
    }
}