using Client.Code.Gameplay.Currency;
using Client.Code.Gameplay.Item;
using UnityEngine;

namespace Client.Code.Gameplay.Shop
{
    [CreateAssetMenu(menuName = "Client/Configs/ShopItem", fileName = "ShopItemConfig", order = 0)]
    public class ShopItemConfig : ScriptableObject
    {
        public string Id;
        public ItemAmount ToInventory; //valid only if not IsCurrency!
        public CurrencyAmount ToWallet; //valid only if IsCurrency!
        public CurrencyAmount Price;
        public string Name;
        [TextArea] public string Description;
        public bool IsCurrency;
        public bool IsEndless;

        private void OnValidate()
        {
            if (IsCurrency)
            {
                if (ToInventory.Config)
                {
                    Debug.LogWarning($"Cant use {nameof(ToInventory)} when IsCurrency");
                    ToInventory.Config = null;
                }
            }
            else if (ToWallet.Config)
            {
                Debug.LogWarning($"Cant use {nameof(ToWallet)} when not IsCurrency");
                ToWallet.Config = null;
            }
        }
    }
}