using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Client.Code.Gameplay.Restaurant
{
    public class RestaurantController : MonoBehaviour
    {
        public Transform EnterPoint;
        public Transform ExitPoint;
        public List<RestaurantCustomerTableController> Tables;

        public bool HasEmptyTable() => Tables.Any(x => x.IsEmpty);

        public RestaurantCustomerTableController ReserveEmptyTable()
        {
            var table = Tables.First(x => x.IsEmpty);
            table.Reserve();
            return table;
        }
    }
}