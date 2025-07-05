using System;
using Client.Code.Core.Dispose;
using UnityEngine;
using UnityEngine.Pool;

namespace Client.Code.Gameplay.Craft
{
    public class CraftPanel : MonoBehaviour, IDisposable
    {
        public CraftItemView ItemView; //Of course, I should to display a list of crafts, but I don't have much time.
        private CraftController _craftController;
        private readonly CompositeDisposable _disposables = new();

        public void Construct(CraftController craftController) => _craftController = craftController;

        public void Initialize()
        {
            using var d = ListPool<CraftItemController>.Get(out var controllers);
            _craftController.GetAll(controllers);

            for (var i = 0; i < controllers.Count; i++)
            {
                if (i < 1)
                {
                    ItemView.Initialize(controllers[i]);
                    ItemView.AddTo(_disposables);
                }
            }
        }

        public void Dispose() => _disposables.Dispose();
    }
}