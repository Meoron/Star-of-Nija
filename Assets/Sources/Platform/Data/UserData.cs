namespace Sources.Platform.Data {
    public partial class UserData {
        public int SlotId;
        public int UserId;
        public ulong AccountId;

        public bool IsOnline;
        public bool IsPrimary;
        
        public string DisplayName;

        public ProfileData Profile;
        public GamePadData GamePad;
    }
}