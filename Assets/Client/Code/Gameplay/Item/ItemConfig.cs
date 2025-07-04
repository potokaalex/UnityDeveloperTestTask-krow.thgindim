using UnityEngine;

namespace Client.Code.Gameplay.Item
{
    [CreateAssetMenu(menuName = "Client/Configs/Item", fileName = "ItemConfig", order = 0)]
    public class ItemConfig : ScriptableObject
    {
        public string Id;
        public Sprite Icon;
        public string Name;
        [TextArea] public string Description;
    }
}