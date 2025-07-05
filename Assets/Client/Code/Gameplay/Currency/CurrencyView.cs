using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Client.Code.Gameplay.Currency
{
    public class CurrencyView : MonoBehaviour
    {
        public Image Icon;
        public TextMeshProUGUI Count;

        public void View(CurrencyAmount amount) => View(amount.Config.Icon, amount.Count);

        public void View(CurrencyItem item) => View(item.Icon, item.Count);

        private void View(Sprite icon, int count)
        {
            Icon.sprite = icon;
            Count.SetText(count.ToString());
        }
    }
}