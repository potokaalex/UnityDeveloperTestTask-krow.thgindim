using Client.Code.Core.Dispose;
using Client.Code.Core.UI;
using Client.Code.Gameplay.Currency;
using UnityEngine;

namespace Client.Code.Gameplay.Building
{
    public class BuildingControllerView : MonoBehaviour
    {
        public CurrencyView PriceView;
        public ButtonView PurchaseButton;
        private readonly CompositeDisposable _disposable = new();
        private IBuildingController _controller;
        private bool _canView;

        public void Initialize(IBuildingController controller)
        {
            _controller = controller;
            _controller.OnChanged.Subscribe(UpdateView).When(() => _canView).AddTo(_disposable);
            PurchaseButton.OnClick.Subscribe(controller.TryBuild).AddTo(_disposable);
        }

        public void Dispose() => _disposable.Dispose();

        public void Setup()
        {
            _canView = true;
            UpdateView();
        }

        public void Clear() => _canView = false;

        private void UpdateView()
        {
            gameObject.SetActive(!_controller.IsBuilt);
            PriceView.View(_controller.BuildPrice);
        }
    }
}