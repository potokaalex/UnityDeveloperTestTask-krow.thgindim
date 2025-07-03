using Client.Code.Core.Progress;
using Client.Code.Core.Progress.Actors;
using Client.Code.Core.Rx;

namespace Client.Code.Gameplay.Player
{
    public class PlayerScore : IProgressWriter
    {
        private readonly IProgressProvider _progressProvider;

        public PlayerScore(IProgressProvider progressProvider) => _progressProvider = progressProvider;

        public void Initialize() => Score = _progressProvider.Data.Player.Score;

        public float Score { get; private set; }

        public EventAction OnScoreChanged { get; } = new();
        
        public void Add(float value)
        {
            Score += value;
            OnScoreChanged.Invoke();
        }

        public void OnWrite(ProgressData progress) => progress.Player.Score = Score;
    }
}