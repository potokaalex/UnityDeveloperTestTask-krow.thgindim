using Client.Code.Core.Audio;
using Client.Code.Core.Dispose;
using Client.Code.Core.UI;
using UnityEngine.UI;

namespace Client.Code.Core.Settings
{
    public class SettingsWindow : WindowView
    {
        public Slider AudioSlider;
        public ButtonView ExitButton;
        private readonly CompositeDisposable _disposables = new();
        private AudioController _audioController;

        public void Construct(AudioController audioController) => _audioController = audioController;

        public override void Initialize()
        {
            base.Initialize();
            AudioSlider.minValue = _audioController.MinVolume;
            AudioSlider.maxValue = _audioController.MaxVolume;
            AudioSlider.onValueChanged.AddListener(SetAudioVolume);
            _audioController.OnMasterVolumeChanged.Subscribe(ViewAudio).When(() => IsOpen).AddTo(_disposables);
            ExitButton.OnClick.Subscribe(Close).AddTo(_disposables);
            ViewAudio();
        }

        public void Dispose()
        {
            AudioSlider.onValueChanged.RemoveListener(SetAudioVolume);
            _disposables.Dispose();
        }

        private void SetAudioVolume(float value) => _audioController.MasterVolume = value;

        private void ViewAudio() => AudioSlider.SetValueWithoutNotify(_audioController.MasterVolume);
    }
}