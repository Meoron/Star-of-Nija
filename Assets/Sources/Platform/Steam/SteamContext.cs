#if !DISABLESTEAMWORKS
using Steamworks;

namespace Sources.Platform.Steam {
    public sealed class SteamContext : IPlatformContext {
        public IUserService UserService { get; }
        public ISaveService SaveService { get; }
        public IAchievementService AchievementService { get; }

        private bool _isInitialized;
        public SteamContext() {
            var isInitializationFailed = !Steamworks.SteamAPI.Init();
#if PRODUCTION_BUILD
            isInitializationFailed |= Steamworks.SteamAPI.RestartAppIfNecessary((Steamworks.AppId_t) 2349260)
                                      || !Steamworks.SteamAPI.IsSteamRunning();
            if (isInitializationFailed) {
                UnityEngine.Application.Quit();
                return;
            }
            _isInitialized = true;
#endif
            
            UserService = new SteamUserService();
            SaveService = new SteamSaveService();
            AchievementService = new SteamAchievementService();
        }

        public void Update() {
            if (_isInitialized) {
                SteamAPI.RunCallbacks();
            }
        }
    }
}
#endif