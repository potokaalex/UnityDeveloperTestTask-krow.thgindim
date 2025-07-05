using System.Collections.Generic;
using Client.Code.Core.Scene;
using Client.Code.Gameplay.Craft;
using Client.Code.Gameplay.Currency;
using Client.Code.Gameplay.Item;
using Client.Code.Gameplay.Shop;
using UnityEngine;

namespace Client.Code.Core.Config
{
    [CreateAssetMenu(menuName = "Client/Configs/Main", fileName = "ConfigData", order = 0)]
    public class ConfigData : ScriptableObject
    {
        public List<SerializedKeyValue<SceneName, string>> Scenes;
        public List<ItemConfig> Items;
        public List<ShopItemConfig> ShopItems;
        public List<CurrencyConfig> CurrencyItems;
        public List<CraftItemConfig> CraftItems;
    }
}