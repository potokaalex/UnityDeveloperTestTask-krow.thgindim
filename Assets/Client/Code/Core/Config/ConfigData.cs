using System.Collections.Generic;
using Client.Code.Core.Scene;
using Client.Code.Gameplay.Item;
using UnityEngine;

namespace Client.Code.Core.Config
{
    [CreateAssetMenu(menuName = "Client/Configs/Main", fileName = "ConfigData", order = 0)]
    public class ConfigData : ScriptableObject
    {
        public List<SerializedKeyValue<SceneName, string>> Scenes;
        public List<ItemConfig> Items;
    }
}