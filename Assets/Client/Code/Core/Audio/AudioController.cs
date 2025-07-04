using System.Collections;
using Client.Code.Core.Progress;
using Client.Code.Core.Progress.Actors;
using Client.Code.Core.Rx;
using UnityEngine;
using UnityEngine.Audio;

namespace Client.Code.Core.Audio
{
    public class AudioController : MonoBehaviour, IProgressWriter
    {
        private IProgressProvider _progressProvider;
        public AudioMixerGroup MasterMixer;
        public string MasterVolumeName;
        private float _masterVolume;

        public void Construct(IProgressProvider progressProvider) => _progressProvider = progressProvider;

        public float MinVolume => -80;

        public float MaxVolume => 0;

        public float MasterVolume
        {
            get => _masterVolume;
            set
            {
                _masterVolume = value;
                MasterMixer.audioMixer.SetFloat(MasterVolumeName, value);
                OnMasterVolumeChanged.Invoke();
            }
        }

        public EventAction OnMasterVolumeChanged { get; } = new();

        public void Initialize() => StartCoroutine(InitializeCoroutine());

        public void OnWrite(ProgressData progress) => progress.Audio.MasterVolume = MasterVolume;

        private IEnumerator InitializeCoroutine()
        {
            var progress = _progressProvider.Data.Audio.MasterVolume;
            MasterVolume = progress;
            //It seems the unity audio system initializes after AudioController.Initialize - unity bug ?
            yield return null;
            MasterVolume = progress;
        }
    }
}