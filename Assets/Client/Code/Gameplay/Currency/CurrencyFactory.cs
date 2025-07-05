using Client.Code.Core.Config;

namespace Client.Code.Gameplay.Currency
{
    public class CurrencyFactory
    {
        private readonly IConfigsProvider _configsProvider;

        public CurrencyFactory(IConfigsProvider configsProvider) => _configsProvider = configsProvider;

        public CurrencyConfig GetConfig(CurrencyType type) => _configsProvider.Data.CurrencyItems.Find(x => x.Type == type);

        public CurrencyAmount CreateAmount(CurrencyType type, int count) => new(GetConfig(type), count);
    }
}