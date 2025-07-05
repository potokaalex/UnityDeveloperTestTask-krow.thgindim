using Client.Code.Gameplay.Item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Client.Code.Gameplay.Craft
{
    public class CraftCellView : MonoBehaviour
    {
        public Image Icon;
        public TextMeshProUGUI Count;

        public void Initialize()
        {
            Icon.color = Color.clear;
            Count.SetText("");
        }
        
        public void View(ItemAmount item)
        {
            Icon.sprite = item.Config.Icon;
            Icon.color = Color.white;
            Count.SetText(item.Count.ToString());
        }
    }
}