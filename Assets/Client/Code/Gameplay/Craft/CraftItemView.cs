using System;
using Client.Code.Core.Dispose;
using Client.Code.Core.UI;
using Client.Code.Gameplay.Item;
using UnityEngine;
using UnityEngine.Pool;

namespace Client.Code.Gameplay.Craft
{
    public class CraftItemView : MonoBehaviour, IDisposable
    {
        public CraftCellView[] InputCells;
        public CraftCellView OutputCell;
        public ButtonView CraftButton;
        private readonly CompositeDisposable _disposables = new();
        private CraftItemController _controller;

        public void Initialize(CraftItemController controller)
        {
            _controller = controller;
            CraftButton.OnClick.Subscribe(TryCraft).AddTo(_disposables);
            View();
        }

        public void Dispose() => _disposables.Dispose();
        
        private void TryCraft() => _controller.TryCraft();

        private void View()
        {
            using var d1 = ListPool<ItemAmount>.Get(out var inItems);
            _controller.GetInItems(inItems);

            for (var i = 0; i < inItems.Count; i++)
                if (i < InputCells.Length)
                    InputCells[i].View(inItems[i]);
            OutputCell.View(_controller.OutItem);
        }
    }
}