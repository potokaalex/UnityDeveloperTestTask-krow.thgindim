using Client.Code.Core.ServiceLocator;
using Client.Code.Gameplay.Customer;
using UnityEngine;

namespace Client.Code.Gameplay
{
    public class GameplayInstaller : MonoBehaviour
    {
        public CustomerSpawner CustomerSpawner;
        public RestaurantController RestaurantController;
        public CameraController CameraController;
        private CustomersToRestaurantSender _customersToRestaurantSender;

        public void Awake()
        {
            //create
            var serviceLocator = new ServiceLocator();

            serviceLocator.Register<CameraController>(CameraController);
            
            serviceLocator.Register<RestaurantController>(RestaurantController);
            
            var customerContainer = new CustomersContainer();
            serviceLocator.Register<CustomersContainer>(customerContainer);
            
            var customerFactory = new CustomerFactory(serviceLocator);
            CustomerSpawner.Construct(customerFactory);
            
            _customersToRestaurantSender = new CustomersToRestaurantSender(customerContainer, RestaurantController);
            
            //init
            CustomerSpawner.Initialize();
        }

        public void Update() => _customersToRestaurantSender.Tick();
    }
}