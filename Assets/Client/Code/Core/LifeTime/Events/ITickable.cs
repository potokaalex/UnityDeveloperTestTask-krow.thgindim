namespace Client.Code.Core.LifeTime.Events
{
    public interface ITickable : ILifeEvent
    {
        void Tick();
    }
}