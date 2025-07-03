using System.Collections.Generic;
using Client.Code.Core.Progress;
using Client.Code.Core.Rx;
using Client.Code.Gameplay.Player;
using UnityEngine;

namespace Client.Code.Gameplay.CustomerZone
{
    public class CustomerTableController : MonoBehaviour, IPlayerInteractive
    {
        public int Id;
        public Transform CustomerPoint;
        public bool IsAlive;
        private IProgressProvider _progressProvider;

        public bool IsEmpty { get; private set; } = true;

        public EventAction OnInteract { get; } = new();

        public void Construct(IProgressProvider progressProvider) => _progressProvider = progressProvider;

        public void Initialize()
        {
            var progress = _progressProvider.Data.Restaurant.CustomerTables;
            var progressIndex = progress.FindIndex(x => x.Id == Id);
            if (progressIndex >= 0)
                IsAlive = progress[progressIndex].IsAlive;
            gameObject.SetActive(IsAlive);
        }

        public void Reserve() => IsEmpty = false;

        public void Clear() => IsEmpty = true;

        public void Interact() => OnInteract.Invoke();

        public void Build()
        {
            gameObject.SetActive(true);
            IsAlive = true;
        }

        public void WriteProgress(List<CustomerTableProgressData> progressData) => progressData.Add(new CustomerTableProgressData(Id, IsAlive));
    }
}