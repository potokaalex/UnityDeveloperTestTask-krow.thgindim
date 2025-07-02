using Client.Code.Core.Config;
using UnityEngine.SceneManagement;

namespace Client.Code.Core.Scene
{
    public class SceneLoader
    {
        private readonly IConfigsProvider _configsProvider;

        public SceneLoader(IConfigsProvider configsProvider) => _configsProvider = configsProvider;

        public void LoadScene(SceneName name) => SceneManager.LoadScene(GetNameStr(name), LoadSceneMode.Single);

        private string GetNameStr(SceneName name) => _configsProvider.Data.Scenes.Find(x => x.Key == name).Value;
    }
}