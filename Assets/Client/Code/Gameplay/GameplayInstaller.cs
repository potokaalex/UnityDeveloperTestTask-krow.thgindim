using Client.Code.Core.Audio;
using Client.Code.Core.Config;
using Client.Code.Core.Dispose;
using Client.Code.Core.LifeTime.Events;
using Client.Code.Core.Progress;
using Client.Code.Core.Progress.Actors;
using Client.Code.Core.Scene;
using Client.Code.Core.ServiceLocatorCode;
using Client.Code.Core.Settings;
using Client.Code.Gameplay.Building;
using Client.Code.Gameplay.Craft;
using Client.Code.Gameplay.Currency;
using Client.Code.Gameplay.Customer;
using Client.Code.Gameplay.Home;
using Client.Code.Gameplay.Item;
using Client.Code.Gameplay.Player;
using Client.Code.Gameplay.Player.Inventory;
using Client.Code.Gameplay.Player.Level;
using Client.Code.Gameplay.Player.Wallet;
using Client.Code.Gameplay.Restaurant;
using Client.Code.Gameplay.Restaurant.CustomerZone;
using Client.Code.Gameplay.Restaurant.Kitchen;
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
        public KitchenUpgradeWindow KitchenUpgradeWindow;
        private readonly CompositeDisposable _disposables = new();
        private ProgressController _progressController;
        private ItemsProvider _itemsFactory;
        private CurrencyFactory _currencyFactory;
        private PlayerScore _playerScore;
        private PlayerWallet _playerWallet;
        private PlayerInventory _playerInventory;
        private PlayerLevel _playerLevel;
        private IConfigsProvider _configsProvider;

        protected override void Install()
        {
            _progressController = Locator.Get<ProgressController>();
            _configsProvider = Locator.Get<IConfigsProvider>();

            _itemsFactory = new ItemsProvider(_configsProvider);
            _currencyFactory = new CurrencyFactory(_configsProvider);

            TryRegister(CameraController);

            InstallPlayer();
            InstallCustomer();
            InstallRestaurant();
            InstallShop();

            var craftController = new CraftController(_playerInventory, _playerScore, _configsProvider);
            TryRegister(craftController);

            var gameplayManager = new GameplayManager(Locator.Get<SceneLoader>(), _progressController);

            SettingsWindow.Construct(Locator.Get<AudioController>());
            TryRegister(SettingsWindow);

            InventoryWindow.Construct(_playerInventory, craftController);
            TryRegister(InventoryWindow);

            BuildingWindow.Construct(CustomerZoneController);
            TryRegister(BuildingWindow);

            HomeWindow.Construct(gameplayManager, SettingsWindow, ShopWindow, InventoryWindow, _playerLevel, _playerWallet, BuildingWindow);
            TryRegister(HomeWindow);
        }

        protected override void UnInstall() => _disposables.Dispose();

        private void TryRegister(object service)
        {
            if (service is ILifeEvent lifeEvent)
                LifeTime.Register(lifeEvent);
            if (service is IProgressActor progressActor)
                _progressController.Register(progressActor).AddTo(_disposables);
        }

        private void InstallPlayer()
        {
            _playerInventory = new PlayerInventory(_progressController, _itemsFactory);
            TryRegister(_playerInventory);

            _playerWallet = new PlayerWallet(_progressController, _currencyFactory);
            TryRegister(_playerWallet);

            _playerLevel = new PlayerLevel(_progressController, _currencyFactory, _playerWallet);
            TryRegister(_playerLevel);

            _playerScore = new PlayerScore(_playerLevel);

            var playerRaycaster = new PlayerRaycaster(CameraController);
            TryRegister(playerRaycaster);

            PlayerLevelCongratulationWindow.Construct(_playerLevel);
            TryRegister(PlayerLevelCongratulationWindow);
        }

        private void InstallCustomer()
        {
            var customerContainer = new CustomersContainer();
            var customerFactory = new CustomerFactory(customerContainer, RestaurantController, CameraController, KitchenController, _playerScore,
                _playerWallet, CustomerZoneController);

            CustomerSpawner.Construct(customerFactory);
            TryRegister(CustomerSpawner);

            var customersToRestaurantSender = new CustomersToRestaurantSender(customerContainer, CustomerZoneController);
            TryRegister(customersToRestaurantSender);
        }

        private void InstallRestaurant()
        {
            KitchenController.Construct(CameraController, _playerWallet, _progressController, _currencyFactory);
            TryRegister(KitchenController);

            KitchenUpgradeWindow.Construct(KitchenController);
            TryRegister(KitchenUpgradeWindow);

            CustomerZoneController.Construct(_progressController, _playerScore, _playerWallet, _currencyFactory);
            TryRegister(CustomerZoneController);
        }

        private void InstallShop()
        {
            var shopController = new ShopController(_configsProvider, _playerInventory, _progressController, _playerScore, _playerWallet);
            TryRegister(shopController);

            ShopWindow.Construct(shopController);
            TryRegister(ShopWindow);
        }
    }
}