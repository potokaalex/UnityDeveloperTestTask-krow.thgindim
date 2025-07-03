using UnityEngine;

namespace Client.Code.Gameplay.Customer
{
    public class CustomerSpawner : MonoBehaviour
    {
        public Vector2 AreaSize;
        public CustomerController Prefab;
        public int Count;
        private CustomerFactory _factory;

        public void Construct(CustomerFactory factory) => _factory = factory;

        public void Initialize()
        {
            var halfSize = new Vector3(AreaSize.x, 0, AreaSize.y) / 2f;
            var min = transform.position - halfSize;
            var max = transform.position + halfSize;

            for (var i = 0; i < Count; i++)
            {
                var position = new Vector3(Random.Range(min.x, max.x), 0, Random.Range(min.z, max.z));
                _factory.Create(Prefab, position, transform, min, max);
            }
        }

        public void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, new Vector3(AreaSize.x, 0, AreaSize.y));
            Gizmos.color = Color.white;
        }
    }
}