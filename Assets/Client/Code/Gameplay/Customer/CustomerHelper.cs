using System;
using Client.Code.Core.BehaviorTree;
using UnityEngine;
using UnityEngine.AI;

namespace Client.Code.Gameplay.Customer
{
    public class CustomerHelper
    {
        private readonly NavMeshAgent _agent;

        public CustomerHelper(NavMeshAgent agent) => _agent = agent;

        public bool GoingToRestaurant { get; set; } //It is better to make a model with data, like CustomerModel : MonoBehaviour

        public bool AgentAtTarget() => (_agent.destination - _agent.transform.position).magnitude < 0.1f;

        public INode MoveTo(Func<Vector3> getPoint)
        {
            return new WaitUntilNode(() =>
            {
                SetDestination(getPoint.Invoke());
                return AgentAtTarget();
            });
        }

        public void SetDestination(Vector3 target) => _agent.SetDestination(target);
    }
}