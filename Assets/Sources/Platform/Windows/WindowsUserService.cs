using System;
using Sources.Platforms.Data;

namespace Sources.Platforms.Windows {
    public sealed class WindowsUserService : IUserService {
        public UserData[] Users { get; private set; }
        public event Action<UserData, LoginState> UserStatusChanged;
        

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
            
            UserStatusChanged?.Invoke(user, LoginState.SignedIn);
        }

        public void Logout(int slot){
            var user = GetUser(slot);
            UserStatusChanged?.Invoke(user, LoginState.SignedIn);
        }

        public UserData GetUser(int slot) {
            return Users[slot];
        }

        public UserData GetPrimaryUser(){
            throw new NotImplementedException();
        }
    }
}