using Client.Code.Gameplay.Player;
using Client.Code.Gameplay.Player.Wallet;
using Client.Code.Gameplay.Restaurant;
using Client.Code.Gameplay.Restaurant.CustomerZone;
using Client.Code.Gameplay.Restaurant.Kitchen;
using UnityEngine;

namespace Client.Code.Gameplay.Customer
{
    public class CustomerFactory
    {
        private readonly RestaurantController _restaurantController;
        private readonly CameraController _cameraController;
        private readonly KitchenController _kitchenController;
        private readonly PlayerScore _playerScore;
        private readonly PlayerWallet _playerWallet;
        private readonly CustomerZoneController _customerZoneController;
        private readonly CustomersContainer _customersContainer;

        public CustomerFactory(CustomersContainer customersContainer, RestaurantController restaurantController, CameraController cameraController,
            KitchenController kitchenController, PlayerScore playerScore, PlayerWallet playerWallet, CustomerZoneController customerZoneController)
        {
            _customersContainer = customersContainer;
            _restaurantController = restaurantController;
            _cameraController = cameraController;
            _kitchenController = kitchenController;
            _playerScore = playerScore;
            _playerWallet = playerWallet;
            _customerZoneController = customerZoneController;
        }

        public void Create(CustomerController prefab, Vector3 position, Transform root, Vector3 areaMin, Vector3 areaMax)
        {
            var controller = Object.Instantiate(prefab, root, true);
            controller.transform.position = position;
            controller.Construct(_restaurantController, _cameraController, _kitchenController, _playerScore, _playerWallet, _customerZoneController,
                areaMin, areaMax);
            controller.Initialize();
            _customersContainer.Add(controller);
        }
    }
}