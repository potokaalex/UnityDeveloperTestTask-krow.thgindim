using Client.Code.Gameplay.Item;
using UnityEngine;

namespace Client.Code.Gameplay.Craft
{
    [CreateAssetMenu(menuName = "Client/Configs/CraftItem", fileName = "CraftItemConfig", order = 0)]
    public class CraftItemConfig : ScriptableObject
    {
        public ItemAmount[] InItems;
        public ItemAmount OutItem;

        private void OnValidate()
        {
            var maxInItems = 3;
            
            if (InItems.Length > maxInItems)
            {
                var inItems = new ItemAmount[3];
                for (var i = 0; i < maxInItems; i++) 
                    inItems[i] = InItems[i];
                
                InItems = inItems;
                Debug.LogWarning($"Maximum {nameof(InItems)} count is {maxInItems}.");
            }
        }
    }
}