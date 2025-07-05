using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Client.Code.Gameplay.Currency
{
    public class CurrencyView : MonoBehaviour
    {
        public Image Icon;
        public TextMeshProUGUI Count;

        public void View(CurrencyItem item)
        {
            Icon.sprite = item.Icon;
            Count.SetText(item.Count.ToString());
        }
    }
}