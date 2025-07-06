namespace Client.Code.Core.LifeTime.Events
{
    public interface IInitializable : ILifeEvent
    {
        void Initialize();
    }
}