using Client.Code.Gameplay.Player.Level;

namespace Client.Code.Gameplay.Player
{
    public class PlayerScore
    {
        private readonly PlayerLevel _playerLevel;

        public PlayerScore(PlayerLevel playerLevel) => _playerLevel = playerLevel;

        public void Add(float value) => _playerLevel.AddProgress(value);
    }
}