﻿using Client.Code.Core.ServiceLocatorCode;
using Client.Code.Gameplay.CustomerZone;
using Client.Code.Gameplay.Kitchen;
using Client.Code.Gameplay.Player;
using Client.Code.Gameplay.Player.Wallet;
using Client.Code.Gameplay.Restaurant;
using UnityEngine;

namespace Client.Code.Gameplay.Customer
{
    public class CustomerFactory
    {
        private readonly ServiceLocator _serviceLocator;

        public CustomerFactory(ServiceLocator serviceLocator) => _serviceLocator = serviceLocator;

        public void Create(CustomerController prefab, Vector3 position, Transform root, Vector3 areaMin, Vector3 areaMax)
        {
            var controller = Object.Instantiate(prefab, root, true);
            controller.transform.position = position;
            controller.Construct(_serviceLocator.Get<RestaurantController>(), _serviceLocator.Get<CameraController>(),
                _serviceLocator.Get<KitchenController>(), _serviceLocator.Get<PlayerScore>(), _serviceLocator.Get<PlayerWallet>(),
                _serviceLocator.Get<CustomerZoneController>(), areaMin, areaMax);
            controller.Initialize();
            _serviceLocator.Get<CustomersContainer>().Add(controller);
        }
    }
}