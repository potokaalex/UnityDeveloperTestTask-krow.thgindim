using UnityEngine;

namespace Client.Code.Core.Config
{
    public class ConfigsController : IConfigsProvider
    {
        private ConfigData _configData;

        ConfigData IConfigsProvider.Data => _configData;

        public void Initialize() => _configData = Resources.Load<ConfigData>("ConfigData");
    }
}