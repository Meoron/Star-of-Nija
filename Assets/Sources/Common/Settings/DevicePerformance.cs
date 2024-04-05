using UnityEngine;

public sealed class DevicePerformance {
    public static bool IsPPESupported { get; }
    public static bool IsStandalonePlatform { get; }
    
    public static Vector2Int RenderTextureSize { get; }
    public static Vector2Int NativeScreenResolution { get; }
    public static Vector2Int MinScreenResolution { get; }
    public static Vector2Int MaxScreenResolution { get; }
    
    public static PerformanceMode PreferredPerformanceMode { get; }
    
    static DevicePerformance() {
        IsStandalonePlatform = Application.platform == RuntimePlatform.WindowsPlayer
                               || Application.platform == RuntimePlatform.LinuxPlayer
                               || Application.platform == RuntimePlatform.OSXPlayer;

        IsPPESupported = SystemInfo.supportsImageEffects;
        RenderTextureSize = NativeScreenResolution = MinScreenResolution = MaxScreenResolution = new Vector2Int(Screen.width, Screen.height);
        
#if UNITY_ANDROID
        if(SystemInfo.processorFrequency <= 1700 || !SystemInfo.supportsImageEffects || SystemInfo.systemMemorySize < 2000)
            PreferredPerformanceMode = PerformanceMode.ULTRA_LOW;
        
        if (SystemInfo.graphicsMemorySize <= 1024) {
            PreferredPerformanceMode = PerformanceMode.LOW;
        }

        if (SystemInfo.graphicsMemorySize > 1024 && SystemInfo.graphicsMemorySize < 2048) {
            PreferredPerformanceMode = PerformanceMode.MEDIUM;
        }

        if (SystemInfo.graphicsMemorySize >= 2048) {
            PreferredPerformanceMode = PerformanceMode.HIGH;
        }

#elif UNITY_TVOS
        PreferredPerformanceMode = PerformanceMode.HIGH;// UnityEngine.tvOS.Device.generation >= UnityEngine.tvOS.DeviceGeneration.AppleTV2Gen ? PerformanceMode.HIGH : PerformanceMode.MEDIUM;
#elif UNITY_IOS
        var deviceGPUName = SystemInfo.graphicsDeviceName;
        var match = System.Text.RegularExpressions.Regex.Match(deviceGPUName, @"\d{2}");
            
        PreferredPerformanceMode = match.Success ? PerformanceMode.HIGH : PerformanceMode.MEDIUM;
#elif UNITY_SWITCH
		PreferredPerformanceMode = PerformanceMode.HIGH;
#elif UNITY_STANDALONE
        if (SystemInfo.graphicsMemorySize <= 1024) {
            PreferredPerformanceMode = PerformanceMode.LOW;
        }

        if (SystemInfo.graphicsMemorySize > 1024 && SystemInfo.graphicsMemorySize < 2048) {
            PreferredPerformanceMode = PerformanceMode.MEDIUM;
        }

        if (SystemInfo.graphicsMemorySize >= 2048) {
            PreferredPerformanceMode = PerformanceMode.HIGH;
        }
#else
		PreferredPerformanceMode = PerformanceMode.HIGH;
#endif
    }
}

public enum PerformanceMode {
    ULTRA_LOW = 0,
    LOW = 1,
    MEDIUM = 2,
    HIGH = 3
}