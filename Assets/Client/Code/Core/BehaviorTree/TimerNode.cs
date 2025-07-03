using System;
using Client.Code.Gameplay.Customer;
using UnityEngine;

namespace Client.Code.Core.BehaviorTree
{
    public class TimerNode : INode
    {
        private readonly float _initialTime;
        private readonly Action<float> _onTimeTick;
        private readonly Action _onEnd;
        private float _time;

        public TimerNode(float initialTime, Action<float> onTimeTick, Action onEnd)
        {
            _initialTime = initialTime;
            _onTimeTick = onTimeTick;
            _onEnd = onEnd;
        }

        public NodeState Tick()
        {
            if (_time >= _initialTime)
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