namespace Sources.Platforms.Windows {
    public sealed class DefaultWindowsServices : IPlatformServices {
        public IUserService UserService { get; }
        public ISaveService SaveService { get; }
        public IAchievementService AchievementService { get; }

        public DefaultWindowsServices() {
            UserService = new DefaultWindowsUserService();
            SaveService = new DefaultWindowsSaveService();
            AchievementService = new DefaultWindowsAchievementService();
        }
        
        public void Update() {
        }
    }
}