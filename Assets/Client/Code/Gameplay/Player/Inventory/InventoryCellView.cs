using UnityEngine;
using UnityEngine.UI;

namespace Client.Code.Gameplay.Player.Inventory
{
    public class InventoryCellView : MonoBehaviour
    {
        public Image Icon;
        
        public void Initialize() => Clear();

        public void View(InventoryItem item)
        {
            Icon.sprite = item.Config.Icon;
            Icon.color = Color.white;
        }

        public void Clear() => 
            Icon.color = Color.clear;
    }
}