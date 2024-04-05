using System.Threading.Tasks;
using System.Xml;
using Sources.Common;
using Sources.Project.Game.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sources.Project.Scenes{
	public interface ISceneController : IUpdateable{
		IProjectContext ProjectContext{ get; }
		ISceneContext SceneContext{ get; }

		string SceneName{ get; }

		Task Initialize(IProjectContext fsContext, string sceneName);
		Task Release();
	}

	public class SceneController : MonoBehaviour, ISceneController{
		[SerializeField] protected CameraController _cameraController;

        public IProjectContext ProjectContext { get; protected set; }
        public ISceneContext SceneContext { get; protected set; }

        public string SceneName { get; private set; }
        
        public bool IsInitialized { get; private set; }
        public bool IsActivated { get; private set; }
        
        public string Player;
        public string AmbientMusic;
        //public VolumeProfile PPEProfile;

        public Vector3 SpawnPosition;
        public Vector3 SpawnRotation;

        public async Task Initialize(IProjectContext projectContext, string sceneName) {
            ProjectContext = projectContext;
            SceneName = sceneName;
            SceneContext = new SceneContext(projectContext, _cameraController);
            
            await OnInitialize();
        }

        public async Task Release() {
            //SceneContext.UpdateManager.Stop();
            //SceneContext.Systems.Release();
            
            OnRelease();
        }

        protected virtual async Task OnInitialize() {
            SceneManager.sceneLoaded += SceneManager_OnSceneLoaded;
            SceneManager.sceneUnloaded += SceneManager_OnSceneUnloaded;
            SceneManager.activeSceneChanged += SceneManager_OnActiveSceneChanged;

            await LoadConfig($"Configs/Locations/{SceneName}");

            /*var ppeVolume = GetComponent<Volume>();
            if (ppeVolume == null) {
                ppeVolume = gameObject.AddComponent<Volume>();
            }
            ppeVolume.profile = PPEProfile;*/

            SceneContext.PlayerController = await LoadPlayer();
            
            //SceneContext.CameraController.Initialize(SceneContext.PlayerController);

            IsInitialized = true;
            
            //SceneContext.CameraController.ReplaceTargetTransform(SceneContext.PlayerController.Transform);
            //SceneContext.UpdateManager.Play();
            //FSContext.AudioManager.Play(AmbientMusic, 0.5f, true, SoundType.Music);
        }

        protected virtual async Task OnRelease() {
            SceneManager.sceneLoaded -= SceneManager_OnSceneLoaded;
            SceneManager.sceneUnloaded -= SceneManager_OnSceneUnloaded;
            SceneManager.activeSceneChanged -= SceneManager_OnActiveSceneChanged;

            //FSContext.AudioManager.StopAllSounds();
            //SceneContext.UpdateManager.Stop();
            IsInitialized = false;
        }

        protected async Task LoadConfig(string fileName) {
            var config1 = Resources.Load<TextAsset>(fileName);
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(config1.text);
            
            /*var config = Serialization.Deserialize<SceneConfig, XMLSerializationProvider, XmlDocument>(xmlDoc);
            if (config != null) {
                if (config.EnableGameCursor) {
                    await FSContext.WindowManager.Open<CursorWindowController>();
                }

                Player = config.Player.Name;
                SpawnPosition = config.Player.Position.ParseToVector3();
                SpawnRotation = config.Player.Rotation.ParseToVector3();

                AmbientMusic = config.AmbientMusic;

                if (!string.IsNullOrEmpty(config.PPEProfile)) {
                    var profile = await AssetManager.LoadAsync<VolumeProfile>(config.PPEProfile);
                    if (profile != null) {
                        PPEProfile = profile;
                    }
                }

                var account = FSContext.AccountManager.GetPrimaryAccount();
                var villageInteractions = account.Progress.VillageInteractions.InteractionsStatus;
                
                foreach (var entity in config.Entities) {
                    var prefab = await AssetManager.LoadPrefabAsync<SceneEntity>(entity.Prefab);
                    var sceneEntity = Instantiate(
                        prefab,
                        entity.Position.ParseToVector3(),
                        Quaternion.Euler(entity.Rotation.ParseToVector3()),
                        transform);
                    sceneEntity.gameObject.name = entity.Name;

                    /*if (entity.Interaction != null) {
                        var e = sceneEntity as InteractionEntity;
                        e.Link(entity.Interaction);
                        
                        SceneContext.InteractionManager.Add(e);

                        if (e is VillageInteractionEntity) {
                            var villageInteraction = e as VillageInteractionEntity;
                            villageInteraction.IsHasNewUpdate =
                                villageInteractions.Count <= 0 ||
                                villageInteractions.ContainsKey(villageInteraction.Data.Name);
                        }
                    }
                }
            }*/
        }

        protected async Task<IPlayerController> LoadPlayer() {
            /*var prefab = await AssetManager.LoadPrefabAsync<SceneEntity>(Player);
            var player = (IPlayerController)Instantiate(prefab, SpawnPosition, Quaternion.Euler(SpawnRotation));
            player.Initialize(FSContext, SceneContext);

            var progress = FSContext.AccountManager.GetPrimaryAccount().Progress;
            player.Account.Experience = progress.Experience;
            player.Account.Money = progress.Money;
            
            var inventory = progress.Inventory.Items;
            if (inventory != null) {
                player.Inventory.LoadItems(inventory);
            }

            return player;*/

            return null;
        }

        protected virtual void OnSceneLoaded() {
        }

        protected virtual void OnSceneUnloaded() {
        }

        protected virtual void OnSceneActivated() {
            if (IsInitialized) {
                //SceneContext.CameraController.Transform.gameObject.SetActive(true);
            }
        }

        protected virtual void OnSceneDeactivated() {
            if (IsInitialized) {
                //SceneContext.CameraController.Transform.gameObject.SetActive(false);
            }
        }

        private void SceneManager_OnActiveSceneChanged(Scene from, Scene to) {
            if (from.name == SceneName && IsActivated) {
                IsActivated = false;
                OnSceneDeactivated();
            }
            else if (to.name == SceneName && !IsActivated) {
                IsActivated = true;
                OnSceneActivated();
            }
        }

        private void SceneManager_OnSceneUnloaded(Scene scene) {
            if (scene.name == SceneName) {
                OnSceneUnloaded();
            }
        }

        private void SceneManager_OnSceneLoaded(Scene scene, LoadSceneMode mode) {
            if (scene.name == SceneName) {
                OnSceneLoaded();
            }
        }

        public void OnUpdate(float deltaTime){
            
        }
    }
}