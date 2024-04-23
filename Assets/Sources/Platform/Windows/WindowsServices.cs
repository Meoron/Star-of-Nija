namespace Sources.Platforms.Windows {
    public sealed class WindowsServices : IPlatformServices {
        public IUserService UserService { get; }
        public ISaveService SaveService { get; }
        public IAchievementService AchievementService { get; }

        public WindowsServices() {
            UserService = new WindowsUserService();
            SaveService = new WindowsSaveService();
            AchievementService = new WindowsAchievementService();
        }
        
        public void Update() {
        }
    }
}