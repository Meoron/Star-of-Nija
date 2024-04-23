namespace Sources.Platforms {
    public interface IPlatformServices {
        IUserService UserService { get; }
        ISaveService SaveService { get; }
        IAchievementService AchievementService { get; }

        void Update();
    }
}