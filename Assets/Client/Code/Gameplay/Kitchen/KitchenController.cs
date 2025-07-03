using System.Collections.Generic;
using Client.Code.Core;
using Client.Code.Core.UI;
using UnityEngine;

namespace Client.Code.Gameplay.Kitchen
{
    public class KitchenController : MonoBehaviour
    {
        public Transform CooksPoint;
        public TimerView TimerView;
        public ToCameraRotator ToCameraRotator;
        private readonly Queue<FoodCreationOrder> _orders = new();

        public float CookingTime => 5f;

        public void Construct(CameraController cameraController) => ToCameraRotator.Construct(cameraController);

        public void Initialize() => TimerView.Hide();

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
    }
}