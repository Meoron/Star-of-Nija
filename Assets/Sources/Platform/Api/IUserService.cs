using System;
using Sources.Platform.Data;

namespace Sources.Platform {
    public interface IUserService {
        event Action<UserData> UserSignedOut;
        event Action<UserData> UserSignedIn;
        event Action<UserData> UserChanged;
        
        UserData[] Users { get; }
        
        bool IsInitialize { get; }
        
        void Login(int slot);
        void Logout(int slot);

        UserData GetUser(int slot);
    }
}