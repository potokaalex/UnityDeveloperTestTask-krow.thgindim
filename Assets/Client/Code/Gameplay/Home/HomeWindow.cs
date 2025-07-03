using Client.Code.Core.Dispose;
using Client.Code.Gameplay.Player;
using TMPro;
using UnityEngine;

namespace Client.Code.Gameplay.Home
{
    public class HomeWindow : MonoBehaviour
    {
        public TextMeshProUGUI Gold;
        private PlayerInventory _playerInventory;
        private readonly CompositeDisposable _disposable = new();

        public void Construct(PlayerInventory playerInventory) => _playerInventory = playerInventory;

        public void Initialize() => _playerInventory.OnChanged.Subscribe(UpdateView).Call().AddTo(_disposable);

        public void Dispose() => _disposable.Dispose();

        private void UpdateView()
        {
            var gold = _playerInventory.Get(InventoryItemType.Gold);
            Gold.SetText($"Gold: {gold.Count}");
        }
    }
}