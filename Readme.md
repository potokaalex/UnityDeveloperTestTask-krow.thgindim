# UnityDeveloperTestTask.
<img width="1612" height="904" alt="image" src="https://github.com/user-attachments/assets/b6175f11-8e42-441d-90ba-94dd86b2af47" />

## Task
Create a basic 3D idle tycoon game (any genre: ex. Mall, car shop, trains business, supermarket etc) Use any free game assets
- Basic set of systems: game core, building/creation system, AI system, inventory system, player resources/balance system, save system, ui, progression/level system
- Create basic UI/UX system (main menu, settings, load game, gameplay ui, etc)
- Create basic building/creation system with expansion possibility (ex: new buildings, new car parkings, new bussines spots)
- Create basic inventory and crafting system (ex: car shopinventory of some parts or resources - creation some items from resources player have)
- Create basic progression system (ex. more business spots or level - more profit and more automation etc)
- Create custom json based save system (auto save) (not using player prefs, not using any assets)
- Show your capabilities in visual part of the project, choose one style (realistic, cartoon, toon, sci-fi, etc) and make the visual part of the whole project in that style.
- Do not use any third-party libraries like: Zenject, Dotween, etc! Use Native libraries only.
Optional: Create custom systems that make the gameplay deeper (examples: currencies exchange/trade, negotiation system, AI bots dialog system, and more custom systems)

## Problems 
- Binding(install). Solution - zenject, or expand the current solution with locators and contexts.
- Items and currencies. Solution - separate data and logic, operate only with controllers, example: _inventory.Add(_factory.CreateController(someConfigData), count).
- Overload CustomerController. Solution - create a model with data, like CustomerModel : MonoBehaviour
- Shop. Completely separate the currency exchanger, or allow to always buy currencies and items in any shopItem.
- Duplicate logic in windows. Solution - expand WindowView?

## Time
Deadline time: 5 days

## Result
Employer's refusal, no more feedback :/
