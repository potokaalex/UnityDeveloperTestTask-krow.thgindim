using System;
using System.Collections.Generic;
using Client.Code.Gameplay.CustomerZone;

namespace Client.Code.Gameplay.Restaurant
{
    [Serializable]
    public class RestaurantProgressData
    {
        public List<CustomerTableProgressData> CustomerTables = new();
        public bool HasReception;
    }
}