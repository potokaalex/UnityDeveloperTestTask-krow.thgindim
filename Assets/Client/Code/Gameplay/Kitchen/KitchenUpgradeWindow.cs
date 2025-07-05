using Client.Code.Core.Dispose;
using Client.Code.Core.Settings;
using Client.Code.Core.UI;
using Client.Code.Gameplay.Currency;
using TMPro;

namespace Client.Code.Gameplay.Kitchen
{
    public class KitchenUpgradeWindow : WindowView
    {
        public ButtonView CloseButton;
        public TextMeshProUGUI LevelText;
        public TextMeshProUGUI CookingTimeText;
        public CurrencyView PriceView;
        public ButtonView PurchaseButton;
        private KitchenController _kitchenController;
        private readonly CompositeDisposable _disposables = new();

        public void Construct(KitchenController kitchenController) => _kitchenController = kitchenController;

        public override void Initialize()
        {
            base.Initialize();
            CloseButton.OnClick.Subscribe(Close).AddTo(_disposables);
            _kitchenController.OnInteract.Subscribe(Open).AddTo(_disposables);
            PurchaseButton.OnClick.Subscribe(_kitchenController.TryUpgradeLevel).AddTo(_disposables);
            _kitchenController.OnLevelChanged.Subscribe(UpdateView).AddTo(_disposables);
        }

        public override void Open()
        {
            base.Open();
            UpdateView();
        }

        private void UpdateView()
        {
            LevelText.SetText($"Level: {_kitchenController.Level}");
            PriceView.View(_kitchenController.Price);
            CookingTimeText.SetText(
                $"CookingTime: {_kitchenController.CookingTime:F1} {_kitchenController.CookingTimeUpgradeGrowth:F1}");
        }

        public void Dispose() => _disposables.Dispose();
    }
}