using Client.Code.Gameplay.Restaurant;
using UnityEngine;
using UnityEngine.Pool;

namespace Client.Code.Gameplay.Customer
{
    public class CustomersToRestaurantSender
    {
        private readonly CustomersContainer _container;
        private readonly CustomerZoneController _customerZoneController;

        public CustomersToRestaurantSender(CustomersContainer container, CustomerZoneController customerZoneController)
        {
            _container = container;
            _customerZoneController = customerZoneController;
        }

        public void Tick()
        {
            using var d = ListPool<CustomerController>.Get(out var allControllers);
            _container.GetAll(allControllers);

            using var d2 = ListPool<CustomerController>.Get(out var validControllers);
            foreach (var controller in allControllers)
            {
                if (controller.CanGoRestaurant)
                    validControllers.Add(controller);
            }

            while (validControllers.Count > 0 && _customerZoneController.HasEmptyTable())
            {
                var index = Random.Range(0, validControllers.Count);
                validControllers[index].GoRestaurant(_customerZoneController.ReserveEmptyTable());
                validControllers.RemoveAt(index);
            }
        }
    }
}