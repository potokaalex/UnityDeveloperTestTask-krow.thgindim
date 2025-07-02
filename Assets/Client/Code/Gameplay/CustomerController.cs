using UnityEngine;
using UnityEngine.AI;

namespace Client.Code.Gameplay
{
    public class CustomerController : MonoBehaviour
    {
        public Transform Target;
        public NavMeshAgent NavMeshAgent;

        private void Update() => NavMeshAgent.SetDestination(Target.position);
    }
}