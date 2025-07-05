using System.Collections.Generic;
using Client.Code.Gameplay.Item;
using Client.Code.Gameplay.Player;
using Client.Code.Gameplay.Player.Inventory;

namespace Client.Code.Gameplay.Craft
{
    public class CraftItemController
    {
        private readonly CraftItemConfig _config;
        private readonly PlayerInventory _playerInventory;
        private readonly PlayerScore _playerScore;

        public CraftItemController(CraftItemConfig config, PlayerInventory playerInventory, PlayerScore playerScore)
        {
            _config = config;
            _playerInventory = playerInventory;
            _playerScore = playerScore;
        }

        public ItemAmount OutItem => _config.OutItem;

        public void GetInItems(List<ItemAmount> inItems)
        {
            inItems.Clear();
            inItems.AddRange(_config.InItems);
        }

        public bool TryCraft()
        {
            foreach (var inItem in _config.InItems)
                if (!_playerInventory.Has(inItem))
                    return false;

            foreach (var inItem in _config.InItems)
                _playerInventory.Remove(inItem);

            _playerInventory.Add(OutItem);
            _playerScore.Add(5);
            return true;
        }
    }
}