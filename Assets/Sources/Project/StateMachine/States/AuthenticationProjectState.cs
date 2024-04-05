using System;
using Sources.Common;
using Sources.Common.Serialization;
using Sources.Platforms.Data;
using Sources.Project.Data;
using Sources.Project.Game;
using Sources.Project.StateMachine;

namespace Sources.Project{
	public sealed class AuthenticationProjectState : ProjectState{
		public override void Initialize(Common.StateMachine.StateMachine stateMachine){
            base.Initialize(stateMachine);
            
			ProjectContext.PlatformContext.UserService.UserStatusChanged += UserService_UserServiceOnUserSignedIn;
			ProjectContext.PlatformContext.UserService.Login(0);
            
            _stateMachine.ApplyState<LobbyProjectState>();
		}

		public override void Release(){
		
		}

		public override void OnUpdate(float deltaTime){
		
		}
		
		private async void UserService_UserServiceOnUserSignedIn(int slotId, LoginState loginState) {
            ProjectContext.PlatformContext.UserService.UserStatusChanged -= UserService_UserServiceOnUserSignedIn;
            var userData = ProjectContext.PlatformContext.UserService.GetPrimaryUser();
            var save = await ProjectContext.PlatformContext.SaveService.Read(userData.UserId, 1, "test");
            var account = Serialization.Deserialize<Account, BinarySerializationProvider, byte[]>(save);
            if (account == null) {
                account = new Account();
            }
            
            /*if (account.Version < GameConfig.SaveVersion) {
                account = new Account();
                account.Version = GameConfig.SaveVersion;
            }*/
            
            ProjectContext.AccountManager.Register(account, userData.SlotId);
            
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

            foreach (var quest in account.Progress.Quests.ActiveQuests) {
                var activeQuest = FSContext.QuestManager.GetActiveQuest(quest.Name);
                if (activeQuest == null) {
                    activeQuest = FSContext.QuestManager.StartQuest(quest);
                }
                activeQuest.Data.Progress = quest.Progress;
                activeQuest.Data.IsCompleted = quest.IsCompleted;
            }*/
            _stateMachine.ApplyState<LobbyProjectState>();
        }
	}
}