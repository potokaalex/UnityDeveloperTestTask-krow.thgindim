namespace Client.Code.Core.ServiceLocatorCode
{
    public abstract class ProjectContext : Context
    {
        public override void Awake()
        {
            DontDestroyOnLoad(this);
            Locator = new ServiceLocator();
            base.Awake();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            Locator.Clear();
            Locator = null;
        }
    }
}