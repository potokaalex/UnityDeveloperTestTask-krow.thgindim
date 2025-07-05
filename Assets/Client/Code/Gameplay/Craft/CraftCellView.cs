using Client.Code.Gameplay.Item;
using UnityEngine;
using UnityEngine.UI;

namespace Client.Code.Gameplay.Craft
{
    public class CraftCellView : MonoBehaviour
    {
        public Image Icon;

        public void View(ItemAmount item)
        {
            Icon.sprite = item.Config.Icon;
            Icon.color = Color.white;
        }
    }
}