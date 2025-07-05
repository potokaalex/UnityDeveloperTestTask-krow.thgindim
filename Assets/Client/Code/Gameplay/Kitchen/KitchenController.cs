using System.Collections.Generic;
using Client.Code.Core;
using Client.Code.Core.Progress;
using Client.Code.Core.Progress.Actors;
using Client.Code.Core.Rx;
using Client.Code.Core.UI;
using Client.Code.Gameplay.Currency;
using Client.Code.Gameplay.Player;
using Client.Code.Gameplay.Player.Wallet;
using UnityEngine;

namespace Client.Code.Gameplay.Kitchen
{
    public class KitchenController : MonoBehaviour, IProgressWriter, IPlayerInteractive
    {
        public Transform CooksPoint;
        public TimerView TimerView;
        public ToCameraRotator ToCameraRotator;
        private readonly Queue<FoodCreationOrder> _orders = new();
        private PlayerWallet _playerWallet;
        private IProgressProvider _progressProvider;
        private CurrencyFactory _currencyFactory;

        public int Level { get; private set; }

        public EventAction OnInteract { get; } = new();

        public EventAction OnLevelChanged { get; } = new();

        public CurrencyAmount Price => _currencyFactory.CreateAmount(CurrencyType.Gem, PriceCount);

        private int PriceCount => (int)(10f - 9f * Mathf.Exp(-0.05f * (Level - 1)));

        public float CookingTime => GetCookingTime(Level);

        public float CookingTimeUpgradeGrowth => GetCookingTime(Level + 1) - CookingTime;

        public void Construct(CameraController cameraController, PlayerWallet playerWallet, IProgressProvider progressProvider,
            CurrencyFactory currencyFactory)
        {
            _currencyFactory = currencyFactory;
            _progressProvider = progressProvider;
            _playerWallet = playerWallet;
            ToCameraRotator.Construct(cameraController);
        }

        public void Initialize()
        {
            Level = _progressProvider.Data.Restaurant.KitchenLevel;
            TimerView.Hide();
        }

        public void Update()
        {
            if (_orders.Count > 0)
            {
                var order = _orders.Peek();
                order.MoveProgress(Time.deltaTime);

                TimerView.Show();
                TimerView.View(order.CookingTime, order.ProgressTime);

                if (order.IsReady)
                {
                    _orders.Dequeue();
                    TimerView.Hide();
                }
            }
        }

        public FoodCreationOrder CreateOrder()
        {
            var order = new FoodCreationOrder(CookingTime);
            _orders.Enqueue(order);
            return order;
        }

        public void TryUpgradeLevel()
        {
            if (_playerWallet.Remove(Price))
            {
                Level++;
                OnLevelChanged.Invoke();
            }
        }

        public void Interact() => OnInteract.Invoke();

        public void OnWrite(ProgressData progress) => progress.Restaurant.KitchenLevel = Level;

        private float GetCookingTime(int level) => 1f + 4f * Mathf.Exp(-0.05f * (level - 1));
    }
}