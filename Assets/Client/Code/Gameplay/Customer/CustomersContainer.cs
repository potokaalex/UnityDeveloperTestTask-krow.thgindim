using System.Collections.Generic;

namespace Client.Code.Gameplay.Customer
{
    public class CustomersContainer
    {
        private readonly List<CustomerController> _items = new();

        public void Add(CustomerController item) => _items.Add(item);

        public void GetAll(List<CustomerController> outList)
        {
            outList.Clear();
            outList.AddRange(_items);
        }
    }
}