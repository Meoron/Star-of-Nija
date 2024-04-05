using System.Collections.Generic;

namespace Sources.Platforms.Data {
    public sealed class SaveData {
        public int SlotId;
        public Dictionary<string, byte[]> Data;
    }
}