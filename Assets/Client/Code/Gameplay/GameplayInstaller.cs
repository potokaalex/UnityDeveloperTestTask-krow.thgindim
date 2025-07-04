using Client.Code.Core.Dispose;
using Client.Code.Core.Progress;
using Client.Code.Core.Scene;
using Client.Code.Core.ServiceLocatorCode;
using Client.Code.Gameplay.Customer;
using Client.Code.Gameplay.CustomerZone;
using Client.Code.Gameplay.Home;
using Client.Code.Gameplay.Kitchen;
using Client.Code.Gameplay.Player;
using Client.Code.Gameplay.Restaurant;

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
        private CustomersToRestaurantSender _customersToRestaurantSender;
        private PlayerRaycaster _playerRaycaster;
        private readonly CompositeDisposable _disposables = new();

        protected override void Install()
        {
            //create
            var progressController = Locator.Get<ProgressController>();
            var playerInventory = new PlayerInventory(progressController);
            var playerScore = new PlayerScore(progressController);
            KitchenController.Construct(CameraController);
            var customerContainer = new CustomersContainer();
            var customerFactory = new CustomerFactory(Locator);
            CustomerSpawner.Construct(customerFactory);
            _customersToRestaurantSender = new CustomersToRestaurantSender(customerContainer, CustomerZoneController);
            CustomerZoneController.Construct(progressController, playerScore);
            _playerRaycaster = new PlayerRaycaster(CameraController);
            var gameplayManager = new GameplayManager(Locator.Get<SceneLoader>(), progressController);
            HomeWindow.Construct(playerInventory, CustomerZoneController, playerScore, gameplayManager);

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
            
            playerInventory.Initialize();
            playerScore.Initialize();
            KitchenController.Initialize();
            CustomerZoneController.Initialize();
            CustomerSpawner.Initialize();
            HomeWindow.Initialize();
        }

        protected override void UnInstall()
        {
            HomeWindow.Dispose();
            _disposables.Dispose();
        }

        public void Update()
        {
            _customersToRestaurantSender.Tick();
            _playerRaycaster.Tick();
        }
    }
}