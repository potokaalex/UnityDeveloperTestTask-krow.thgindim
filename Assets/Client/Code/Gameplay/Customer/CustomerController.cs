using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Client.Code.Gameplay.Customer
{
    public class CustomerController : MonoBehaviour
    {
        public NavMeshAgent NavMeshAgent;
        private bool _isRight;
        private Vector3 _areaMin;
        private Vector3 _areaMax;

        public void Initialize(Vector3 areaMin, Vector3 areaMax)
        {
            _areaMin = areaMin;
            _areaMax = areaMax;
            _isRight = Random.value > 0.5f;
            SetDestination();
        }

        private void Update()
        {
            if ((NavMeshAgent.destination - transform.position).magnitude < 0.01f)
            {
                _isRight = !_isRight;
                SetDestination();
            }
        }

        private void SetDestination()
        {
            var target = new Vector3(_isRight ? _areaMax.x : _areaMin.x, 0, Random.Range(_areaMin.z, _areaMax.z));
            NavMeshAgent.SetDestination(target);
        }
    }
}