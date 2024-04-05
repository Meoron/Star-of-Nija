using System;
using Sources.Platforms.Data;

namespace Sources.Platforms {
    public interface IUserService {
        event Action<int, LoginState> UserStatusChanged;
        
        UserData[] Users { get; }
        
        bool IsInitialize { get; }
        
        void Login(int slot);
        void Logout(int slot);

        void Release();
        void Update(float deltaTime);
        
        UserData GetUser(int slot);
        UserData GetPrimaryUser();
    }
}