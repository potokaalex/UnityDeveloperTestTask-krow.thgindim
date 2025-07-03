using Client.Code.Core.ServiceLocator;
using Client.Code.Gameplay.Customer;
using Client.Code.Gameplay.Kitchen;
using Client.Code.Gameplay.Restaurant;
using UnityEngine;

namespace Client.Code.Gameplay
{
    public class GameplayInstaller : MonoBehaviour
    {
        public CustomerSpawner CustomerSpawner;
        public RestaurantController RestaurantController;
        public CameraController CameraController;
        public KitchenController KitchenController;
        private CustomersToRestaurantSender _customersToRestaurantSender;

        public void Awake()
        {
            //create
            var serviceLocator = new ServiceLocator();

            serviceLocator.Register<CameraController>(CameraController);
            serviceLocator.Register<RestaurantController>(RestaurantController);
            KitchenController.Construct(CameraController);
            serviceLocator.Register<KitchenController>(KitchenController);

            var customerContainer = new CustomersContainer();
            serviceLocator.Register<CustomersContainer>(customerContainer);

            var customerFactory = new CustomerFactory(serviceLocator);
            CustomerSpawner.Construct(customerFactory);

            _customersToRestaurantSender = new CustomersToRestaurantSender(customerContainer, RestaurantController);

            //init
            KitchenController.Initialize();
            CustomerSpawner.Initialize();
        }

        public void Update() => _customersToRestaurantSender.Tick();
    }
}