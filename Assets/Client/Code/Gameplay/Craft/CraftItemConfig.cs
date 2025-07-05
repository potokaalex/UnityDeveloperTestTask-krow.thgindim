using System;
using Client.Code.Gameplay.Item;

namespace Client.Code.Gameplay.Craft
{
    [Serializable]
    public class CraftItemConfig
    {
        public ItemAmount[] InItems;
        public ItemAmount OutItem;
    }
}