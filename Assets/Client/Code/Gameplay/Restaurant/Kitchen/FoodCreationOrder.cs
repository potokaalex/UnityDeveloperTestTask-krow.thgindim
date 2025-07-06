using UnityEngine;

namespace Client.Code.Gameplay.Restaurant.Kitchen
{
    public class FoodCreationOrder
    {
        public FoodCreationOrder(float cookingTime) => CookingTime = cookingTime;

        public float CookingTime { get; }

        public float ProgressTime { get; private set; }

        public float RemainingTime => Mathf.Max(0, CookingTime - ProgressTime);

        public bool IsReady => ProgressTime >= CookingTime;

        public void MoveProgress(float deltaTime) => ProgressTime += deltaTime;
    }
}