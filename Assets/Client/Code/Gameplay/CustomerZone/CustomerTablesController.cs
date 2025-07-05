using System.Collections.Generic;
using System.Linq;
using Client.Code.Core.Progress;
using Client.Code.Core.Rx;
using Client.Code.Gameplay.Building;
using Client.Code.Gameplay.Currency;
using Client.Code.Gameplay.Player;
using Client.Code.Gameplay.Player.Wallet;
using Client.Code.Gameplay.Restaurant;
using UnityEngine;

namespace Client.Code.Gameplay.CustomerZone
{
    public class CustomerTablesController : MonoBehaviour, IBuildingController
    {
        public List<CustomerTableController> Tables;
        private PlayerScore _playerScore;
        private PlayerWallet _playerWallet;
        private CurrencyFactory _currencyFactory;

        public bool IsBuilt => BuiltCount >= MaxCount;

        public CurrencyAmount BuildPrice => _currencyFactory.CreateAmount(CurrencyType.Cash, (int)Mathf.Pow(2, BuiltCount - 1));

        public EventAction OnChanged { get; } = new();

        private int BuiltCount => Tables.Count(x => x.IsAlive);

        private int MaxCount => Tables.Count;

        public void Construct(IProgressProvider progressProvider, PlayerScore playerScore, PlayerWallet playerWallet, CurrencyFactory currencyFactory)
        {
            for (var i = 0; i < Tables.Count; i++)
                Tables[i].Construct(progressProvider);
            _playerScore = playerScore;
            _playerWallet = playerWallet;
            _currencyFactory = currencyFactory;
        }

        public void Initialize()
        {
            for (var i = 0; i < Tables.Count; i++)
                Tables[i].Initialize();
        }
        
        public bool HasEmpty() => Tables.Any(x => x.IsEmpty && x.IsAlive);

        public CustomerTableController ReserveEmpty()
        {
            var table = Tables.First(x => x.IsEmpty && x.IsAlive);
            table.Reserve();
            return table;
        }

        public void TryBuild()
        {
            if (BuiltCount < MaxCount && _playerWallet.Remove(BuildPrice))
            {
                Tables.First(x => !x.IsAlive).Build();
                _playerScore.Add(3);
                OnChanged.Invoke();
            }
        }

        public void WriteProgress(RestaurantProgressData progress)
        {
            progress.CustomerTables.Clear();
            for (var i = 0; i < Tables.Count; i++)
                Tables[i].WriteProgress(progress.CustomerTables);
        }
    }
}