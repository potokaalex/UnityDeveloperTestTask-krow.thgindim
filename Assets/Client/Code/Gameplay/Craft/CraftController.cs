using System.Collections.Generic;
using Client.Code.Core.Config;
using Client.Code.Gameplay.Player;
using Client.Code.Gameplay.Player.Inventory;

namespace Client.Code.Gameplay.Craft
{
    public class CraftController
    {
        private readonly List<CraftItemController> _controllers = new();
        private readonly IConfigsProvider _configsProvider;
        private readonly PlayerInventory _playerInventory;
        private readonly PlayerScore _playerScore;

        public CraftController(PlayerInventory playerInventory, PlayerScore playerScore, IConfigsProvider configsProvider)
        {
            _playerInventory = playerInventory;
            _playerScore = playerScore;
            _configsProvider = configsProvider;
        }

        public void Initialize()
        {
            foreach (var config in _configsProvider.Data.CraftItems)
                _controllers.Add(new CraftItemController(config, _playerInventory, _playerScore));
        }

        public void GetAll(List<CraftItemController> outList)
        {
            outList.Clear();
            outList.AddRange(_controllers);
        }
    }
}