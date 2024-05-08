using System;

namespace Sources.Project.Data {
    [Serializable]
    public sealed class Account {
        public int Version { get; set; }
        public int SlotId { get; set; }
        
        public long TimeTicks { get; set; }
        
        public SettingsData Settings;
        public GameProgressData Progress = new GameProgressData();
    }

    [Serializable]
    public sealed class GameProgressData {
        public int Level { get; set; } = 1;
        public int Money { get; set; } = 200;
        public int Experience { get; set; } = 0;
        
        public string LastLocation { get; set; }
    }
}