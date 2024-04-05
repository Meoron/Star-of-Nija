using System;

namespace Sources.Project.Data {
    [Serializable]
    public class SettingsData {
        public float MusicVolume = 0.75f;
        public float SFXVolume = 0.75f;
        public float UIVolume = 0.75f;

        public string Language;
        
        public string Resolution;
        public int VSync;
        public int FPSTarget;
        public bool PostProcessEffect;
        public int AntiAliasing;
        public int ShadowQuality;
        public int TextureQuality;
    }
}
