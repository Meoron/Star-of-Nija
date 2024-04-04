#if !DISABLESTEAMWORKS
using System;
using Sources.Platform.Data;
using Steamworks;
using UnityEngine;

namespace Sources.Platform.Steam {
    public sealed class SteamUserService : IUserService {
        public event Action<UserData> UserSignedOut;
        public event Action<UserData> UserSignedIn;
        public event Action<UserData> UserChanged;

        public UserData[] Users { get; private set; }

        public bool IsInitialize { get; }

        
        protected Callback<AvatarImageLoaded_t> m_AvatarImageLoaded;
        
        public SteamUserService() {
            Users = new UserData[4];
            IsInitialize = true;
            
            
        }

        private void OnAvatarImageLoaded(AvatarImageLoaded_t param) {
            Debug.Log("AAA");
        }

        public void Login(int slot) {
            m_AvatarImageLoaded = Callback<AvatarImageLoaded_t>.Create(OnAvatarImageLoaded);
            
            var user = new UserData {
                    DisplayName = SteamFriends.GetPersonaName(),
                    GamePad = new GamePadData { SlotId = 0 },
                    IsPrimary = true,
                    Profile = new ProfileData(),
            };
            
            var iImage = SteamFriends.GetLargeFriendAvatar(SteamUser.GetSteamID());
            if (iImage > 0) {
                var isValid = SteamUtils.GetImageSize(iImage, out var width, out var height);
                if (isValid) {
                    var avatar = new byte[4 * (int) width * (int) height];
                    isValid = SteamUtils.GetImageRGBA(iImage, avatar, avatar.Length);
                    if (isValid) {
                        var texture = new Texture2D((int)width,(int)height, TextureFormat.RGBA32, false, true);
                        texture.LoadRawTextureData(avatar);
                        user.Profile.Avatar = texture.EncodeToPNG();
                    }
                }
            }
            
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
#endif