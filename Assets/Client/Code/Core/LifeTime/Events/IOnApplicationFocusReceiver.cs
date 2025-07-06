namespace Client.Code.Core.LifeTime.Events
{
    public interface IOnApplicationFocusReceiver : ILifeEvent
    {
        void OnApplicationFocus(bool hasFocus);
    }
}