using System;

namespace Client.Code.Gameplay.Item
{
    [Serializable]
    public struct ItemAmount
    {
        public ItemConfig Item;
        public int Count;
    }
}