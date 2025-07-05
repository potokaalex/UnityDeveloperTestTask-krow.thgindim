using System;
using Client.Code.Core.Dispose;
using UnityEngine;
using UnityEngine.Pool;

namespace Client.Code.Gameplay.Craft
{
    public class CraftPanel : MonoBehaviour, IDisposable
    {
        public Transform ItemsRoot;
        public CraftItemView ItemViewPrefab;
        private CraftController _craftController;
        private readonly CompositeDisposable _disposables = new();

        public void Construct(CraftController craftController) => _craftController = craftController;

        public void Initialize()
        {
            using var d = ListPool<CraftItemController>.Get(out var controllers);
            _craftController.GetAll(controllers);

            for (var i = 0; i < controllers.Count; i++)
            {
                var view = Instantiate(ItemViewPrefab, ItemsRoot);
                view.Initialize(controllers[i]);
                view.AddTo(_disposables);
            }
        }

        public void Dispose() => _disposables.Dispose();
    }
}