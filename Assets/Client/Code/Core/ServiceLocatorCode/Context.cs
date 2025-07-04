using UnityEngine;

namespace Client.Code.Core.ServiceLocatorCode
{
    public abstract class Context : MonoBehaviour
    {
        protected static ServiceLocator Locator;

        public virtual void Awake() => Install();

        public virtual void OnDestroy() => UnInstall();

        protected virtual void Install()
        {
        }

        protected virtual void UnInstall()
        {
        }
    }
}