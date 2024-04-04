using System.Collections.Generic;

namespace Sources.Platform.Data {
    public sealed class SaveData {
        public int SlotId;
        public Dictionary<string, byte[]> Data;
    }
}