using System;
using System.Threading.Tasks;
using Sources.Common;
using Sources.Common.Serialization;
using Sources.Common.StateMachine;
using Sources.Platforms;
using Sources.Platforms.Data;
using Sources.Project.Data;
using Sources.Project.Game;
using Sources.Project.Managers;

namespace Sources.Project.StateMachine{
	public sealed class AuthenticationProjectState : IState{
        private IStateMachine _projectStateMachine;
        private readonly IUserService _userService;
        private readonly ISaveService _saveService;
        private readonly IAccountManager _accountManger;
        
        public AuthenticationProjectState(ProjectStateMachine stateMachine, IPlatformServices platformServices, IAccountManager accountManger){
            _projectStateMachine = stateMachine;
            _userService = platformServices.UserService;
            _saveService = platformServices.SaveService;
            _accountManger = accountManger;
        }
        
        
		public void Enter(){
            _userService.UserStatusChanged += UserService_UserServiceOnUserSignedIn;
            _userService.Login(0);
		}

		public void Exit(){
		
		}

        private async void UserService_UserServiceOnUserSignedIn(UserData userData, LoginState loginState){
            await LoadingUserSaveData(userData);
            MoveToNextState(loginState);
        }
        
		private async Task LoadingUserSaveData(UserData userData) {
            _userService.UserStatusChanged -= UserService_UserServiceOnUserSignedIn;
            var save = await _saveService.Read(userData.UserId, userData.SlotId, "test");
            var account = Serialization.Deserialize<Account, BinarySerializationProvider, byte[]>(save);
            if (account == null) {
                account = new Account();
            }
            
            /*if (account.Version < GameConfig.SaveVersion) {
                account = new Account();
                account.Version = GameConfig.SaveVersion;
            }*/
            
            _accountManger.Register(account, userData.SlotId);
            
            if (account.TimeTicks == -1) {
                account.TimeTicks = DateTime.UtcNow.Ticks;
            }
            
            if (account.Settings == null) {
                var preset = (PresetSettings) null;
                var performanceMode = DevicePerformance.PreferredPerformanceMode;
                if (performanceMode == PerformanceMode.ULTRA_LOW || performanceMode == PerformanceMode.LOW) {
                    preset = GameSettings.Presets[0];
                }
                else if (performanceMode == PerformanceMode.MEDIUM) {
                    preset = GameSettings.Presets[1];
                }
                else if (performanceMode == PerformanceMode.HIGH) {
                    preset = GameSettings.Presets[2];
                }
                
                account.Settings = new SettingsData {
                    FPSTarget = Array.IndexOf(GameSettings.FPS, "60"),
                    Resolution = GameSettings.Resolution,
                    //Language = LocalizationService.DefaultLocalizationName,
                    VSync = preset.VSync,
                    PostProcessEffect = preset.PostProcessEffect,
                    AntiAliasing = preset.AntiAliasing,
                    ShadowQuality = preset.ShadowQuality,
                    TextureQuality = preset.TextureQuality,
                };
            }
            
            GameSettings.MusicVolume = account.Settings.MusicVolume;
            GameSettings.SFXVolume = account.Settings.SFXVolume;
            GameSettings.UIVolume = account.Settings.UIVolume;

            GameSettings.Resolution = account.Settings.Resolution;
            GameSettings.Language = account.Settings.Language;
            
            GameSettings.VSync = account.Settings.VSync;
            GameSettings.FPSTarget = account.Settings.FPSTarget;
            GameSettings.PostProcessEffect = account.Settings.PostProcessEffect;
            GameSettings.AntiAliasing = account.Settings.AntiAliasing;
            GameSettings.ShadowQuality = account.Settings.ShadowQuality;
            GameSettings.TextureQuality = account.Settings.TextureQuality;
            
            //ProjectContext.TimeManager.Reset(account.TimeTicks);
            
            /*ProjectContext.AudioManager.SetVolume(SoundType.Music,  GameSettings.MusicVolume);
            ProjectContext.AudioManager.SetVolume(SoundType.SFX, GameSettings.SFXVolume);
            ProjectContext.AudioManager.SetVolume(SoundType.UI, GameSettings.UIVolume);*/
            
            /*foreach (var quest in account.Progress.Quests.CompletedQuest) {
                FSContext.QuestManager.AddCompletedQuest(quest);
            }
            */
        }
        
        private void MoveToNextState(LoginState loginState){
            if (loginState == LoginState.SignedIn){
                _projectStateMachine.EnterState<LobbyProjectState>();
            }
        }
	}
}