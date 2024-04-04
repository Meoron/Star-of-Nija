using System;
using Sources.Platform.Data;

namespace Sources.Platform.Windows {
    public sealed class WindowsUserService : IUserService {
        public event Action<UserData> UserSignedOut;
        public event Action<UserData> UserSignedIn;
        public event Action<UserData> UserChanged;

        public UserData[] Users { get; private set; }

        public bool IsInitialize { get; }

        public WindowsUserService() {
            Users = new UserData[4];
            IsInitialize = true;
        }
        
        public void Login(int slot) {
            var user = new UserData {
                    DisplayName = $"User_{DateTime.Now.Ticks}",
                    GamePad = new GamePadData { SlotId = 0 },
                    IsPrimary = true,
                    Profile = new ProfileData()
            };
            Users[slot] = user;
            UserSignedIn?.Invoke(user);
        }

        public void Logout(int slot) {
        }

        public UserData GetUser(int slot) {
            return Users[slot];
        }
    }
}