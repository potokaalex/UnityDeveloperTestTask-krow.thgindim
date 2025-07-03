using Client.Code.Core.Progress;
using Client.Code.Core.ServiceLocator;
using Client.Code.Gameplay.Customer;
using Client.Code.Gameplay.CustomerZone;
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
        public CustomerZoneController CustomerZoneController;
        public CameraController CameraController;
        public KitchenController KitchenController;
        public HomeWindow HomeWindow;
        private CustomersToRestaurantSender _customersToRestaurantSender;
        private PlayerRaycaster _playerRaycaster;
        private ProgressController _progressProvider;

        public void Awake()
        {
            //create
            var serviceLocator = new ServiceLocator();

            _progressProvider = new ProgressController();
            
            serviceLocator.Register<CameraController>(CameraController);
            serviceLocator.Register<RestaurantController>(RestaurantController);
            KitchenController.Construct(CameraController);
            serviceLocator.Register<KitchenController>(KitchenController);

            var customerContainer = new CustomersContainer();
            serviceLocator.Register<CustomersContainer>(customerContainer);

            var customerFactory = new CustomerFactory(serviceLocator);
            CustomerSpawner.Construct(customerFactory);

            _customersToRestaurantSender = new CustomersToRestaurantSender(customerContainer, CustomerZoneController);
            CustomerZoneController.Construct(_progressProvider);
            
            var playerInventory = new PlayerInventory(_progressProvider);
            serviceLocator.Register<PlayerInventory>(playerInventory);
            _playerRaycaster = new PlayerRaycaster(CameraController);

            HomeWindow.Construct(playerInventory, CustomerZoneController);

            //init
            _progressProvider.Initialize();
            _progressProvider.RegisterActor(playerInventory);
            _progressProvider.RegisterActor(CustomerZoneController);
            
            playerInventory.Initialize();
            KitchenController.Initialize();
            CustomerZoneController.Initialize();
            CustomerSpawner.Initialize();
            HomeWindow.Initialize();
        }

        public void Update()
        {
            _customersToRestaurantSender.Tick();
            _playerRaycaster.Tick();
        }

        public void OnDestroy() => HomeWindow.Dispose();

        private void OnApplicationFocus(bool hasFocus) => _progressProvider.OnApplicationFocus(hasFocus);
    }
}