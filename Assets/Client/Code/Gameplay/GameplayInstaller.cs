using Client.Code.Core.ServiceLocator;
using Client.Code.Gameplay.Customer;
using Client.Code.Gameplay.Home;
using Client.Code.Gameplay.Kitchen;
using Client.Code.Gameplay.Player;
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
        public HomeWindow HomeWindow;
        private CustomersToRestaurantSender _customersToRestaurantSender;
        private PlayerRaycaster _playerRaycaster;

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

            var playerInventory = new PlayerInventory();
            serviceLocator.Register<PlayerInventory>(playerInventory);
            _playerRaycaster = new PlayerRaycaster(CameraController);

            HomeWindow.Construct(playerInventory);

            //init
            KitchenController.Initialize();
            CustomerSpawner.Initialize();
            HomeWindow.Initialize();
        }

        public void Update()
        {
            _customersToRestaurantSender.Tick();
            _playerRaycaster.Tick();
        }

        public void OnDestroy() => HomeWindow.Dispose();
    }
}