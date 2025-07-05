using System;
using UnityEngine.Serialization;

namespace Client.Code.Gameplay.Item
{
    [Serializable]
    public struct ItemAmount
    {
        [FormerlySerializedAs("Item")] public ItemConfig Config;
        public int Count;
    }
}