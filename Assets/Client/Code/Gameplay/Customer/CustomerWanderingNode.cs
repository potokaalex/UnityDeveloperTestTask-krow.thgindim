using UnityEngine;

namespace Client.Code.Gameplay.Customer
{
    public class CustomerWanderingNode : INode
    {
        private readonly CustomerHelper _helper;
        private readonly Vector3 _areaMin;
        private readonly Vector3 _areaMax;
        private SequenceNode _sequence;
        private bool _isRight;

        public CustomerWanderingNode(CustomerHelper helper, Vector3 areaMin, Vector3 areaMax)
        {
            _helper = helper;
            _areaMin = areaMin;
            _areaMax = areaMax;
        }

        public void Initialize()
        {
            _sequence = new SequenceNode(
                new ActionNode(() => _isRight = Random.value > 0.5f),
                new WaitUntilNode(() =>
                {
                    if (_helper.AgentAtTarget())
                    {
                        _isRight = !_isRight;
                        var target = new Vector3(_isRight ? _areaMax.x : _areaMin.x, 0, Random.Range(_areaMin.z, _areaMax.z));
                        _helper.SetDestination(target);
                    }

                    return _helper.GoingToRestaurant;
                }));
        }

        public NodeState Tick() => _sequence.Tick();
    }
}