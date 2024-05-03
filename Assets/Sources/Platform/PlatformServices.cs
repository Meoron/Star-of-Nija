namespace Sources.Platforms {
    public sealed class PlatformServices : IPlatformServices {
        public IUserService UserService { get; }
        public ISaveService SaveService { get; }
        public IAchievementService AchievementService { get; }

        private IPlatformServices _platformServices;
        
        public PlatformServices() {
#if UNITY_GAMECORE && !UNITY_EDITOR
            _platformContext = new Xbox.XboxContext();
#elif UNITY_PLAYSTATION && !UNITY_EDITOR
            _platformContext = new PlayStation.PlayStationContext();
#elif UNITY_SWITCH && !UNITY_EDITOR
            _platformContext = new Switch.SwitchContext();
#elif UNITY_STANDALONE && !DISABLESTEAMWORKS
            _platformContext = new Sources.Platform.Steam.SteamContext();
#else
            _platformServices = new Windows.WindowsServices();
#endif
            UserService = _platformServices.UserService;
            SaveService = _platformServices.SaveService;
            AchievementService = _platformServices.AchievementService;
        }
        
        public void Update() {
            _platformServices.Update();
        }
    }
}