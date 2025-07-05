using System;
using Client.Code.Core.Dispose;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Client.Code.Gameplay.Player.Level
{
    public class PlayerLevelView : MonoBehaviour, IDisposable
    {
        public TextMeshProUGUI Current;
        public TextMeshProUGUI Next;
        public Slider ProgressSlider;
        private readonly CompositeDisposable _disposables = new();
        private PlayerLevel _playerLevel;

        public void Construct(PlayerLevel playerLevel) => _playerLevel = playerLevel;

        public void Initialize()
        {
            _playerLevel.OnChanged.Subscribe(UpdateView).AddTo(_disposables);
            ProgressSlider.minValue = 0;
            UpdateView();
        }

        public void Dispose() => _disposables.Clear();

        private void UpdateView()
        {
            Current.SetText(_playerLevel.Current.ToString());
            Next.SetText(_playerLevel.Next.ToString());
            ProgressSlider.maxValue = _playerLevel.MaxProgress;
            ProgressSlider.value = _playerLevel.Progress;
        }
    }
}