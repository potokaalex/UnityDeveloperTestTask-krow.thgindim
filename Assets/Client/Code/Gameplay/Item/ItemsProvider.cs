using System.Linq;
using Client.Code.Core.Config;

namespace Client.Code.Gameplay.Item
{
    public class ItemsProvider
    {
        private readonly IConfigsProvider _configsProvider;

        public ItemsProvider(IConfigsProvider configsProvider) => _configsProvider = configsProvider;

        public ItemConfig Get(string itemId) => _configsProvider.Data.Items.First(x => x.Id == itemId);
    }
}