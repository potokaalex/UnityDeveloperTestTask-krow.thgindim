using System;
using System.Collections;
using System.Collections.Generic;
using Client.Code.Core.LifeTime.Events;

namespace Client.Code.Core.LifeTime
{
    public class LifeTimeController
    {
        private readonly List<IOnApplicationFocusReceiver> _applicationFocusReceivers = new();
        private readonly List<IInitializable> _initializeReceivers = new();
        private readonly List<IDisposable> _disposeReceivers = new();
        private readonly List<ITickable> _tickableReceivers = new();

        public void Register(ILifeEvent lifeEvent) => FindListsAndDoAction(lifeEvent, (l, e) => l.Add(e));

        public void UnRegister(ILifeEvent lifeEvent) => FindListsAndDoAction(lifeEvent, (l, e) => l.Remove(e));

        public void Initialize()
        {
            for (var i = 0; i < _initializeReceivers.Count; i++)
                _initializeReceivers[i].Initialize();
        }

        public void Tick()
        {
            for (var i = 0; i < _tickableReceivers.Count; i++)
                _tickableReceivers[i].Tick();
        }

        public void OnApplicationFocus(bool hasFocus)
        {
            for (var i = 0; i < _applicationFocusReceivers.Count; i++)
                _applicationFocusReceivers[i].OnApplicationFocus(hasFocus);
        }

        public void Dispose()
        {
            for (var i = 0; i < _disposeReceivers.Count; i++)
                _disposeReceivers[i].Dispose();
        }

        private void FindListsAndDoAction(ILifeEvent lifeEvent, Action<IList, ILifeEvent> action)
        {
            if (lifeEvent is IOnApplicationFocusReceiver)
                action.Invoke(_applicationFocusReceivers, lifeEvent);
            if (lifeEvent is IInitializable)
                action.Invoke(_initializeReceivers, lifeEvent);
            if (lifeEvent is IDisposable)
                action.Invoke(_disposeReceivers, lifeEvent);
            if (lifeEvent is ITickable)
                action.Invoke(_tickableReceivers, lifeEvent);
        }
    }
}