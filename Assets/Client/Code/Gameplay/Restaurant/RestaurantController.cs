using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Client.Code.Gameplay.Customer
{
    public class RestaurantController : MonoBehaviour
    {
        public Transform CooksPoint;
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

        public void CreateOrder()
        {
            
        }
    }
}