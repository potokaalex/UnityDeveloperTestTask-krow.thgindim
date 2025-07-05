using Client.Code.Core.Audio;
using Client.Code.Core.Config;
using Client.Code.Core.Dispose;
using Client.Code.Core.Progress;
using Client.Code.Core.Scene;
using Client.Code.Core.ServiceLocatorCode;
using Client.Code.Core.Settings;
using Client.Code.Gameplay.Building;
using Client.Code.Gameplay.Craft;
using Client.Code.Gameplay.Currency;
using Client.Code.Gameplay.Customer;
using Client.Code.Gameplay.CustomerZone;
using Client.Code.Gameplay.Home;
using Client.Code.Gameplay.Item;
using Client.Code.Gameplay.Kitchen;
using Client.Code.Gameplay.Player;
using Client.Code.Gameplay.Player.Inventory;
using Client.Code.Gameplay.Player.Level;
using Client.Code.Gameplay.Player.Wallet;
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
        public InventoryWindow InventoryWindow;
        public PlayerLevelCongratulationWindow PlayerLevelCongratulationWindow;
        public BuildingWindow BuildingWindow;
        private CustomersToRestaurantSender _customersToRestaurantSender;
        private PlayerRaycaster _playerRaycaster;
        private readonly CompositeDisposable _disposables = new();

        protected override void Install()
        {
            //create
            var progressController = Locator.Get<ProgressController>();
            var itemsFactory = new ItemsProvider(Locator.Get<IConfigsProvider>());
            var playerInventory = new PlayerInventory(progressController, itemsFactory);
            var currencyFactory = new CurrencyFactory(Locator.Get<IConfigsProvider>());
            var playerWallet = new PlayerWallet(progressController, currencyFactory);
            var playerLevel = new PlayerLevel(progressController, currencyFactory, playerWallet);
            var playerScore = new PlayerScore(playerLevel);
            var craftController = new CraftController(playerInventory, playerScore, Locator.Get<IConfigsProvider>());
            KitchenController.Construct(CameraController);
            var customerContainer = new CustomersContainer();
            var customerFactory = new CustomerFactory(Locator);
            CustomerSpawner.Construct(customerFactory);
            _customersToRestaurantSender = new CustomersToRestaurantSender(customerContainer, CustomerZoneController);
            CustomerZoneController.Construct(progressController, playerScore, playerWallet, currencyFactory);
            _playerRaycaster = new PlayerRaycaster(CameraController);
            var gameplayManager = new GameplayManager(Locator.Get<SceneLoader>(), progressController);
            SettingsWindow.Construct(Locator.Get<AudioController>());
            InventoryWindow.Construct(playerInventory, craftController);
            BuildingWindow.Construct(CustomerZoneController);
            HomeWindow.Construct(gameplayManager, SettingsWindow, ShopWindow, InventoryWindow, playerLevel, playerWallet, BuildingWindow);
            var shopController = new ShopController(Locator.Get<IConfigsProvider>(), playerInventory, progressController, playerScore, playerWallet);
            ShopWindow.Construct(shopController);
            PlayerLevelCongratulationWindow.Construct(playerLevel);
            
            //bind
            Locator.Register<PlayerInventory>(playerInventory).AddTo(_disposables);
            Locator.Register<PlayerWallet>(playerWallet).AddTo(_disposables);
            Locator.Register<PlayerScore>(playerScore).AddTo(_disposables);
            Locator.Register<CameraController>(CameraController).AddTo(_disposables);
            Locator.Register<RestaurantController>(RestaurantController).AddTo(_disposables);
            Locator.Register<KitchenController>(KitchenController).AddTo(_disposables);
            Locator.Register<CustomersContainer>(customerContainer).AddTo(_disposables);

            //init
            progressController.RegisterActor(playerInventory).AddTo(_disposables);
            progressController.RegisterActor(playerLevel).AddTo(_disposables);
            progressController.RegisterActor(CustomerZoneController).AddTo(_disposables);
            progressController.RegisterActor(shopController).AddTo(_disposables);
            progressController.RegisterActor(playerWallet).AddTo(_disposables);

            playerInventory.Initialize();
            playerWallet.Initialize();
            playerLevel.Initialize();
            craftController.Initialize();
            KitchenController.Initialize();
            CustomerZoneController.Initialize();
            CustomerSpawner.Initialize();
            SettingsWindow.Initialize();
            InventoryWindow.Initialize();
            HomeWindow.Initialize();
            BuildingWindow.Initialize();
            shopController.Initialize();
            ShopWindow.Initialize();
            PlayerLevelCongratulationWindow.Initialize();
        }

        protected override void UnInstall()
        {
            SettingsWindow.Dispose();
            InventoryWindow.Dispose();
            HomeWindow.Dispose();
            ShopWindow.Dispose();
            BuildingWindow.Dispose();
            _disposables.Dispose();
            PlayerLevelCongratulationWindow.Dispose();
        }

        public void Update()
        {
            _customersToRestaurantSender.Tick();
            _playerRaycaster.Tick();
        }
    }
}