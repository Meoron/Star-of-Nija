namespace Sources.Platforms {
    public interface IAchievementService {
        void SetProgress(int userId, string achievement, int progress);
    }
}