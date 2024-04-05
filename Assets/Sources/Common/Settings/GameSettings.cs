using System;
using Sources.Common;
using UnityEngine;

namespace Sources.Project.Game {
    public static class GameSettings {
        public static string[] Resolutions = {"3840x2160", "2560x1440", "1920x1200", "1920x1080", "1366x768"};
        public static string[] FPS = {"30", "60", "120"};
        public static string[] Languages = {"English", "Ukrainian"};
        public static string[] AntiAliasings = {"Disabled", "2x", "4x"};
        public static string[] Shadows = { "Disabled", "Hard", "Soft"};
        public static string[] Texture = {"Very High", "High", "Medium", "Low"};
        public static string[] PPE  = {  "Disabled", "Enabled"};
        
        public static PresetSettings[] Presets = {
            new(1, false, 0, 0, 3), //Low
            new(1, false, 2, 1, 1), //Medium
            new(1, true, 3, 3, 0), //High
        };

        static GameSettings() {
            Resolutions = Array.ConvertAll(Screen.resolutions, x => $"{x.width} x {x.height}");
        }
        
        public static string Resolution {
            get {
                return $"{Screen.currentResolution.width} x {Screen.currentResolution.height}";
            }
            set {
                var index = Array.IndexOf(Resolutions, value);
                if (index > -1) {
                    var resolution = Screen.resolutions[index];
                    Screen.SetResolution(resolution.width, resolution.height, true);
                }
            }
        }

        public static string Language { get; set; }
        
        public static float MusicVolume { get; set; } = 0.75f;
        public static float SFXVolume { get; set; } = 0.75f;
        public static float UIVolume { get; set; } = 0.75f;

        public static bool PostProcessEffect { get; set; }
        
        public static float Cursor { get; set; } = 1f;
        
        public static int VSync {             
            get { return QualitySettings.vSyncCount; }
            set { QualitySettings.vSyncCount = value; }
        }
        
        public static int FPSTarget {
            get { return Array.IndexOf(FPS, $"{Application.targetFrameRate}"); }
            set { Application.targetFrameRate = int.Parse(FPS[value]); }
        }

        public static int AntiAliasing {
            get { return QualitySettings.antiAliasing / 2; }
            set { QualitySettings.antiAliasing = value * 2; }
        }
        
        public static int ShadowQuality {
            get { return (int)QualitySettings.shadows; }
            set { QualitySettings.shadows = (ShadowQuality)value; }
        }

        public static int TextureQuality {
            get { return QualitySettings.globalTextureMipmapLimit; }
            set { QualitySettings.globalTextureMipmapLimit = value; }
        }
    }
}