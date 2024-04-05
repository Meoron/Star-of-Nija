namespace Sources.Common {
    public sealed class PresetSettings {
        public int VSync { get; set; }
        public bool PostProcessEffect { get; set; }
        public int AntiAliasing { get; set; }
        public int ShadowQuality { get; set; }
        public int TextureQuality { get; set; }

        public PresetSettings(int vSync, bool ppe, int antiAliasing, int shadowQuality, int textureQuality) {
            VSync = vSync;
            PostProcessEffect = ppe;
            AntiAliasing = antiAliasing;
            ShadowQuality = shadowQuality;
            TextureQuality = textureQuality;
        }
    }
}