using Client.Code.Core.Progress;
using Client.Code.Core.Progress.Actors;
using Client.Code.Gameplay.Currency;
using Client.Code.Gameplay.Player;
using Client.Code.Gameplay.Player.Wallet;
using UnityEngine;

namespace Client.Code.Gameplay.CustomerZone
{
    public class CustomerZoneController : MonoBehaviour, IProgressWriter
    {
        public CustomerTablesController Tables;
        public CustomerReceptionTable ReceptionTable;

        public void Construct(IProgressProvider progressProvider, PlayerScore playerScore, PlayerWallet playerWallet, CurrencyFactory currencyFactory)
        {
            Tables.Construct(progressProvider, playerScore, playerWallet, currencyFactory);
            ReceptionTable.Construct(progressProvider, playerWallet, playerScore);
        }

        public void Initialize()
        {
            Tables.Initialize();
            ReceptionTable.Initialize();
        }

        public void OnWrite(ProgressData progress)
        {
            Tables.WriteProgress(progress.Restaurant);
            ReceptionTable.WriteProgress(progress.Restaurant);
        }
    }
}