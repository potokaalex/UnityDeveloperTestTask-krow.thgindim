using System;
using Client.Code.Core.Audio;
using Client.Code.Gameplay.Player;
using Client.Code.Gameplay.Restaurant;

namespace Client.Code.Core.Progress
{
    [Serializable]
    public class ProgressData
    {
        public PlayerProgressData Player = new();
        public RestaurantProgressData Restaurant = new();
        public AudioProgress Audio;
    }
}