using Client.Code.Gameplay.Item;
using Client.Code.Gameplay.Player.Wallet;
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
    }
}