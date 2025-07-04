using Client.Code.Core.Audio;
using Client.Code.Core.Config;
using Client.Code.Core.Dispose;
using Client.Code.Core.Progress;
using Client.Code.Core.Scene;
using Client.Code.Core.ServiceLocatorCode;
using Client.Code.Core.Settings;
using Client.Code.Gameplay.Customer;
using Client.Code.Gameplay.CustomerZone;
using Client.Code.Gameplay.Home;
using Client.Code.Gameplay.Item;
using Client.Code.Gameplay.Kitchen;
using Client.Code.Gameplay.Player;
using Client.Code.Gameplay.Player.Inventory;
using Client.Code.Gameplay.Restaurant;
using Client.Code.Gameplay.Shop;

namespace Client.Code.Gameplay
{
    public class GameplayInstaller : Context
    {
        public CustomerSpawner CustomerSpawner;
        public RestaurantController RestaurantController;
        public CustomerZoneController CustomerZoneController;
        public CameraController CameraController;
        public KitchenController KitchenController;
        public HomeWindow HomeWindow;
        public SettingsWindow SettingsWindow;
        public ShopWindow ShopWindow;
        private CustomersToRestaurantSender _customersToRestaurantSender;
        private PlayerRaycaster _playerRaycaster;
        private readonly CompositeDisposable _disposables = new();

        protected override void Install()
        {
            //create
            var progressController = Locator.Get<ProgressController>();
            var itemsFactory = new ItemsProvider(Locator.Get<IConfigsProvider>());
            var playerInventory = new PlayerInventory(progressController, itemsFactory);
            var playerScore = new PlayerScore(progressController);
            KitchenController.Construct(CameraController);
            var customerContainer = new CustomersContainer();
            var customerFactory = new CustomerFactory(Locator);
            CustomerSpawner.Construct(customerFactory);
            _customersToRestaurantSender = new CustomersToRestaurantSender(customerContainer, CustomerZoneController);
            CustomerZoneController.Construct(progressController, playerScore, playerInventory);
            _playerRaycaster = new PlayerRaycaster(CameraController);
            var gameplayManager = new GameplayManager(Locator.Get<SceneLoader>(), progressController);
            SettingsWindow.Construct(Locator.Get<AudioController>());
            HomeWindow.Construct(playerInventory, CustomerZoneController, playerScore, gameplayManager, SettingsWindow, itemsFactory, ShopWindow);
            ShopWindow.Construct(progressController, Locator.Get<IConfigsProvider>(), playerInventory);
            
            //bind
            Locator.Register<PlayerInventory>(playerInventory).AddTo(_disposables);
            Locator.Register<PlayerScore>(playerScore).AddTo(_disposables);
            Locator.Register<CameraController>(CameraController).AddTo(_disposables);
            Locator.Register<RestaurantController>(RestaurantController).AddTo(_disposables);
            Locator.Register<KitchenController>(KitchenController).AddTo(_disposables);
            Locator.Register<CustomersContainer>(customerContainer).AddTo(_disposables);
            
            //init
            progressController.RegisterActor(playerInventory).AddTo(_disposables);
            progressController.RegisterActor(playerScore).AddTo(_disposables);
            progressController.RegisterActor(CustomerZoneController).AddTo(_disposables);
            progressController.RegisterActor(ShopWindow).AddTo(_disposables);

            playerInventory.Initialize();
            playerScore.Initialize();
            KitchenController.Initialize();
            CustomerZoneController.Initialize();
            CustomerSpawner.Initialize();
            SettingsWindow.Initialize();
            HomeWindow.Initialize();
            ShopWindow.Initialize();
        }

        protected override void UnInstall()
        {
            SettingsWindow.Dispose();
            HomeWindow.Dispose();
            ShopWindow.Dispose();
            _disposables.Dispose();
        }

        public void Update()
        {
            _customersToRestaurantSender.Tick();
            _playerRaycaster.Tick();
        }
    }
}