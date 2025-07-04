using Client.Code.Gameplay.Item;
using UnityEngine;

namespace Client.Code.Gameplay.Shop
{
    [CreateAssetMenu(menuName = "Client/Configs/ShopItem", fileName = "ShopItemConfig", order = 0)]
    public class ShopItemConfig : ScriptableObject
    {
        public string Id;
        public ItemCount ToInventory;
        public ItemCount Price;
        [TextArea] public string Description;
        public bool IsCurrency;
        public bool IsInfinite;
    }
}