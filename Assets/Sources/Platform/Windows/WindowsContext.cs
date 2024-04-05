namespace Sources.Platforms.Windows {
    public sealed class WindowsContext : IPlatformContext {
        public IUserService UserService { get; }
        public ISaveService SaveService { get; }
        public IAchievementService AchievementService { get; }

        public WindowsContext() {
            UserService = new WindowsUserService();
            SaveService = new WindowsSaveService();
            AchievementService = new WindowsAchievementService();
        }
        
        public void Update() {
        }
    }
}