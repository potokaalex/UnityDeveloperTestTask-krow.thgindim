using System;

namespace Client.Code.Gameplay.CustomerZone
{
    [Serializable]
    public struct CustomerTableProgressData
    {
        public int Id;
        public bool IsAlive;

        public CustomerTableProgressData(int id, bool isAlive)
        {
            Id = id;
            IsAlive = isAlive;
        }
    }
}