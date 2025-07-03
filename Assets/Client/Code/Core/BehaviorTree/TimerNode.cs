using System;
using UnityEngine;

namespace Client.Code.Core.BehaviorTree
{
    public class TimerNode : INode
    {
        private readonly Func<float> _getInitialTime;
        private readonly Action<float> _onTimeTick;
        private readonly Action _onEnd;
        private float _time;

        public TimerNode(Func<float> getInitialTime, Action<float> onTimeTick, Action onEnd)
        {
            _getInitialTime = getInitialTime;
            _onTimeTick = onTimeTick;
            _onEnd = onEnd;
        }

        public NodeState Tick()
        {
            if (_time >= _getInitialTime.Invoke())
            {
                _onEnd.Invoke();
                _time = 0;
                return NodeState.Success;
            }

            _time += Time.deltaTime;
            _onTimeTick.Invoke(_time);
            return NodeState.Running;
        }
    }
}