using UnityEngine;

namespace Client.Code.Gameplay.Customer
{
    public class CustomerSpawner : MonoBehaviour
    {
        public Vector2 AreaSize;
        public CustomerController Prefab;
        public int Count;

        public void Start()
        {
            for (var i = 0; i < Count; i++)
                CreateController();
        }

        public void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, new Vector3(AreaSize.x, 0, AreaSize.y));
            Gizmos.color = Color.white;
        }

        private void CreateController()
        {
            var controller = Instantiate(Prefab, transform, true);
            var halfSize = new Vector3(AreaSize.x, 0, AreaSize.y) / 2f;
            var min = transform.position - halfSize;
            var max = transform.position + halfSize;
            controller.transform.position = new Vector3(Random.Range(min.x, max.x), 0, Random.Range(min.z, max.z));
            controller.Initialize(min, max);
        }
    }
}