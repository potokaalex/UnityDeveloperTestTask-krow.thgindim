using System;

namespace Client.Code.Core
{
    [Serializable]
    public struct SerializedKeyValue<TKey, TValue>
    {
        public TKey Key;
        public TValue Value;
    }
}