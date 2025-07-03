using Client.Code.Core.Rx;
using Client.Code.Gameplay.Player;
using UnityEngine;

namespace Client.Code.Gameplay.Restaurant
{
    public class RestaurantCustomerTableController : MonoBehaviour, IPlayerInteractive
    {
        public Transform CustomerPoint;
        public bool IsAlive;

        public bool IsEmpty { get; private set; } = true;

        public EventAction OnInteract { get; } = new();

        public void Initialize() => gameObject.SetActive(IsAlive);

        public void Reserve() => IsEmpty = false;

        public void Clear() => IsEmpty = true;

        public void Interact() => OnInteract.Invoke();

        public void Build()
        {
            gameObject.SetActive(true);
            IsAlive = true;
        }
    }
}