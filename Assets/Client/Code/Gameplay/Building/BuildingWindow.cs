using Client.Code.Core.Dispose;
using Client.Code.Core.Settings;
using Client.Code.Core.UI;
using Client.Code.Gameplay.CustomerZone;

namespace Client.Code.Gameplay.Building
{
    public class BuildingWindow : WindowView
    {
        public ButtonView CloseButton;
        public BuildingControllerView CustomerTable;
        public BuildingControllerView Reception;
        private readonly CompositeDisposable _disposable = new();
        private CustomerZoneController _customerZoneController;

        public void Construct(CustomerZoneController customerZoneController) => _customerZoneController = customerZoneController;

        public override void Initialize()
        {
            base.Initialize();
            CloseButton.OnClick.Subscribe(Close).AddTo(_disposable);
            CustomerTable.Initialize(_customerZoneController.Tables);
            Reception.Initialize(_customerZoneController.ReceptionTable);
        }

        public void Dispose()
        {
            _disposable.Dispose();
            CustomerTable.Dispose();
            Reception.Dispose();
        }

        public override void Open()
        {
            base.Open();
            CustomerTable.Setup();
            Reception.Setup();
        }

        public override void Close()
        {
            base.Close();
            CustomerTable.Clear();
            Reception.Clear();
        }
    }
}