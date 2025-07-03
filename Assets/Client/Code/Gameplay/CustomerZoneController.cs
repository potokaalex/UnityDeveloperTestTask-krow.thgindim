using System.Collections.Generic;
using System.Linq;
using Client.Code.Core.Rx;
using Client.Code.Gameplay.Player;
using UnityEngine;

namespace Client.Code.Gameplay.Restaurant
{
    public class CustomerZoneController : MonoBehaviour
    {
        public List<RestaurantCustomerTableController> Tables;

        public int TablesAliveCount => Tables.Count(x => x.IsAlive);

        public int TablesMaxCount => Tables.Count;

        public InventoryItem TableBuildPrice => new(InventoryItemType.Gold, 0);

        public EventAction OnTableBuild { get; } = new();

        public void Initialize()
        {
            for (var i = 0; i < Tables.Count; i++)
                Tables[i].Initialize();
        }

        public bool HasEmptyTable() => Tables.Any(x => x.IsEmpty && x.IsAlive);

        public RestaurantCustomerTableController ReserveEmptyTable()
        {
            var table = Tables.First(x => x.IsEmpty && x.IsAlive);
            table.Reserve();
            return table;
        }

        public void BuildTable()
        {
            if (TablesAliveCount < TablesMaxCount)
            {
                Tables.First(x => !x.IsAlive).Build();
                OnTableBuild.Invoke();
            }
        }
    }
}