using System.Collections;
using Client.Code.Core.Config;
using UnityEngine.SceneManagement;

namespace Client.Code.Core.Scene
{
    public class SceneLoader
    {
        private readonly IConfigsProvider _configsProvider;
        private readonly CoroutineRunner _coroutineRunner;
        private readonly LoadingScreen _loadingScreen;

        public SceneLoader(IConfigsProvider configsProvider, CoroutineRunner coroutineRunner, LoadingScreen loadingScreen)
        {
            _configsProvider = configsProvider;
            _coroutineRunner = coroutineRunner;
            _loadingScreen = loadingScreen;
        }

        public void Load(SceneName name) => _coroutineRunner.StartCoroutine(LoadCoroutine(name));

        private IEnumerator LoadCoroutine(SceneName name)
        {
            _loadingScreen.Show(true);
            var operation = SceneManager.LoadSceneAsync(GetNameStr(name), LoadSceneMode.Single);

            while (!operation!.isDone)
                yield return null;
            
            _loadingScreen.Hide();
        }
        
        private string GetNameStr(SceneName name) => _configsProvider.Data.Scenes.Find(x => x.Key == name).Value;
    }
}