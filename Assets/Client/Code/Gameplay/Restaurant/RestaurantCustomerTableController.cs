using UnityEngine;

namespace Client.Code.Gameplay.Restaurant
{
    public class RestaurantCustomerTableController : MonoBehaviour
    {
        public Transform CustomerPoint;

        public bool IsEmpty { get; private set; } = true;

        public void Reserve() => IsEmpty = false;

        public void Clear() => IsEmpty = true;
    }
}