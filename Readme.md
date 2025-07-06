Problems: 
- Binding(install). Solution - zenject, or expand the current solution with locators and contexts.
- Items and currencies. Solution - separate data and logic, operate only with controllers, example: _inventory.Add(_factory.CreateController(someConfigData), count).
- Overload CustomerController. Solution - create a model with data, like CustomerModel : MonoBehaviour
- Shop. Completely separate the currency exchanger, or allow to always buy currencies and items in any shopItem.
- Duplicate logic in windows. Solution - expand WindowView?
