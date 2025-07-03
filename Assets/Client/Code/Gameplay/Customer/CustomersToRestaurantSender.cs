using System.Linq;
using Client.Code.Gameplay.Restaurant;
using UnityEngine;
using UnityEngine.Pool;

namespace Client.Code.Gameplay.Customer
{
    public class CustomersToRestaurantSender
    {
        private readonly CustomersContainer _container;
        private readonly RestaurantController _restaurantController;

        public CustomersToRestaurantSender(CustomersContainer container, RestaurantController restaurantController)
        {
            _container = container;
            _restaurantController = restaurantController;
        }

        public void Tick()
        {
            using var d = ListPool<CustomerController>.Get(out var controllers);
            _container.GetAll(controllers);
            if (controllers.Count < 0)
                return;

            while (_restaurantController.HasEmptyTable() && controllers.Any(x => x.CanGoRestaurant))
            {
                var index = Random.Range(0, controllers.Count);
                controllers[index].GoRestaurant(_restaurantController.ReserveEmptyTable());
                controllers.RemoveAt(index);
            }
        }
    }
}