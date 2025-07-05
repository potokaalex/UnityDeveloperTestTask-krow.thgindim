using System.Collections.Generic;
using System.Linq;
using Client.Code.Core.Progress;
using Client.Code.Core.Progress.Actors;
using Client.Code.Core.Rx;
using Client.Code.Gameplay.Currency;
using Client.Code.Gameplay.Player;
using Client.Code.Gameplay.Player.Wallet;
using UnityEngine;

namespace Client.Code.Gameplay.CustomerZone
{
    public class CustomerZoneController : MonoBehaviour, IProgressWriter
    {
        public List<CustomerTableController> Tables;
        public CurrencyAmount TableBuildPrice;
        private PlayerScore _playerScore;
        private PlayerWallet _playerWallet;

        public int TablesAliveCount => Tables.Count(x => x.IsAlive);

        public int TablesMaxCount => Tables.Count;

        public EventAction OnTableBuild { get; } = new();

        public void Construct(IProgressProvider progressProvider, PlayerScore playerScore, PlayerWallet playerWallet)
        {
            _playerWallet = playerWallet;
            _playerScore = playerScore;
            for (var i = 0; i < Tables.Count; i++)
                Tables[i].Construct(progressProvider);
        }

        public void Initialize()
        {
            for (var i = 0; i < Tables.Count; i++)
                Tables[i].Initialize();
        }

        public bool HasEmptyTable() => Tables.Any(x => x.IsEmpty && x.IsAlive);

        public CustomerTableController ReserveEmptyTable()
        {
            var table = Tables.First(x => x.IsEmpty && x.IsAlive);
            table.Reserve();
            return table;
        }

        public void BuildTable()
        {
            if (TablesAliveCount < TablesMaxCount && _playerWallet.Remove(TableBuildPrice))
            {
                Tables.First(x => !x.IsAlive).Build();
                _playerScore.Add(3);
                OnTableBuild.Invoke();
            }
        }

        public void OnWrite(ProgressData progress)
        {
            progress.Restaurant.CustomerTables.Clear();
            for (var i = 0; i < Tables.Count; i++)
                Tables[i].WriteProgress(progress.Restaurant.CustomerTables);
        }
    }
}