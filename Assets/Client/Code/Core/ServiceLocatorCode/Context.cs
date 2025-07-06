using Client.Code.Core.LifeTime;
using UnityEngine;

namespace Client.Code.Core.ServiceLocatorCode
{
    public abstract class Context : MonoBehaviour
    {
        protected static ServiceLocator Locator;
        protected readonly LifeTimeController LifeTime = new();

        public virtual void Awake()
        {
            Install();
            LifeTime.Initialize();
        }

        public virtual void OnDestroy()
        {
            UnInstall();
            LifeTime.Dispose();
        }

        public void Update() => LifeTime.Tick();

        public void OnApplicationFocus(bool hasFocus) => LifeTime.OnApplicationFocus(hasFocus);

        protected virtual void Install()
        {
        }

        protected virtual void UnInstall()
        {
        }
    }
}