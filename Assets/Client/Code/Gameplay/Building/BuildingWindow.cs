using Client.Code.Core.Dispose;
using Client.Code.Core.Settings;
using Client.Code.Core.UI;
using Client.Code.Gameplay.CustomerZone;

namespace Client.Code.Gameplay.Building
{
    public class BuildingWindow : WindowView
    {
        public ButtonView CloseButton;
        public BuildingWindowItem CustomerTable;
        public BuildingWindowItem Waiter;
        public BuildingWindowItem Reception;
        private readonly CompositeDisposable _disposable = new();
        private CustomerZoneController _customerZoneController;

        public void Construct(CustomerZoneController customerZoneController) => _customerZoneController = customerZoneController;

        public override void Initialize()
        {
            base.Initialize();
            CloseButton.OnClick.Subscribe(Close).AddTo(_disposable);
            InitializeCustomerTable();
        }

        public void Dispose() => _disposable.Dispose();

        public override void Open()
        {
            base.Open();
            UpdateView();
        }

        private void UpdateView()
        {
            CustomerTable.gameObject.SetActive(_customerZoneController.TablesBuiltCount < _customerZoneController.TablesMaxCount);
            CustomerTable.PriceView.View(_customerZoneController.TableBuildPrice);
        }

        private void InitializeCustomerTable()
        {
            CustomerTable.PurchaseButton.OnClick.Subscribe(_customerZoneController.TryBuildTable).AddTo(_disposable);
            _customerZoneController.OnTableBuild.Subscribe(UpdateView).When(() => IsOpen).AddTo(_disposable);
        }
    }
}