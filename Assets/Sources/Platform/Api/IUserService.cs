using System;
using Sources.Platforms.Data;

namespace Sources.Platforms {
    public interface IUserService {
        event Action<UserData, LoginState> UserStatusChanged;
        
        UserData[] Users { get; }
        
        bool IsInitialize { get; }
        
        void Login(int slot);
        void Logout(int slot);
        
        UserData GetUser(int slot);
        UserData GetPrimaryUser();
    }
}