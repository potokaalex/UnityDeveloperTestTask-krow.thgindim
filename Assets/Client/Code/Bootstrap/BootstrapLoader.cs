using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Client.Code.Bootstrap
{
    public class BootstrapLoader : MonoBehaviour
    {
#if UNITY_EDITOR
        public void Awake()
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
                return;

            if (FindObjectsOfType<BootstrapLoader>().Except(new[] { this }).Any())
                return;

            foreach (var m in FindObjectsOfType<MonoBehaviour>())
                if (m != this)
                    DestroyImmediate(m);

            DontDestroyOnLoad(this);
            SceneManager.LoadScene(0);
        }
#endif
    }
}