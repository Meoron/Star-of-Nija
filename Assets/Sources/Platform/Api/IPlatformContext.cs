namespace Sources.Platforms {
    public interface IPlatformContext {
        IUserService UserService { get; }
        ISaveService SaveService { get; }
        IAchievementService AchievementService { get; }

        void Update();
    }
}