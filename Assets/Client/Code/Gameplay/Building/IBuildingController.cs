using Client.Code.Core.Rx;
using Client.Code.Gameplay.Currency;

namespace Client.Code.Gameplay.Building
{
    public interface IBuildingController
    {
        bool IsBuilt { get; }
        CurrencyAmount BuildPrice { get; }
        EventAction OnChanged { get; }
        void TryBuild();
    }
}