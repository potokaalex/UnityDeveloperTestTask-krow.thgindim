using UnityEngine;

namespace Client.Code.Gameplay.Currency
{
    [CreateAssetMenu(menuName = "Client/Configs/Currency", fileName = "CurrencyItemConfig", order = 0)]
    public class CurrencyConfig : ScriptableObject
    {
        public CurrencyType Type;
        public Sprite Icon;
    }
}