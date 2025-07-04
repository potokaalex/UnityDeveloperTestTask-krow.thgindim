using Client.Code.Core.Config;

namespace Client.Code.Gameplay.Player.Wallet
{
    public class CurrencyProvider
    {
        private readonly IConfigsProvider _configsProvider;

        public CurrencyProvider(IConfigsProvider configsProvider) => _configsProvider = configsProvider;

        public CurrencyConfig GetConfig(CurrencyType type) => _configsProvider.Data.CurrencyItems.Find(x => x.Type == type);
    }
}