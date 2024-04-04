namespace Sources.Platform {
    public sealed class PlatformContext : IPlatformContext {
        public IUserService UserService { get; }
        public ISaveService SaveService { get; }
        public IAchievementService AchievementService { get; }

        private IPlatformContext _platformContext;
        
        public PlatformContext() {
#if UNITY_GAMECORE && !UNITY_EDITOR
            _platformContext = new Xbox.XboxContext();
#elif UNITY_PLAYSTATION && !UNITY_EDITOR
            _platformContext = new PlayStation.PlayStationContext();
#elif UNITY_SWITCH && !UNITY_EDITOR
            _platformContext = new Switch.SwitchContext();
#elif UNITY_STANDALONE && !DISABLESTEAMWORKS
            _platformContext = new Sources.Platform.Steam.SteamContext();
#else
            _platformContext = new Windows.WindowsContext();
#endif
            UserService = _platformContext.UserService;
            SaveService = _platformContext.SaveService;
            AchievementService = _platformContext.AchievementService;
        }
        
        public void Update() {
            _platformContext.Update();
        }
    }
}