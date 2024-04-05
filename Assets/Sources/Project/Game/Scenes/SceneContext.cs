using Sources.Common.Context;
using Sources.Project.Game.Player;
using UnityEngine;

namespace Sources.Project.Scenes {
    public interface ISceneContext {
        Transform Content { get; }
        
        IProjectContext FSContext { get; }
        
        CameraController CameraController { get; }
        //UpdateManager UpdateManager { get; }

        //ISystems Systems { get; }

        IPlayerController PlayerController { get; set; }
    }

    public sealed class SceneContext : Context, ISceneContext {
        public Transform Content { get; }
        public IProjectContext FSContext { get; }
        public CameraController CameraController { get; }
        
        //public UpdateManager UpdateManager { get; }
        
        //public ISystems Systems { get; }
        
        public IPlayerController PlayerController { get; set; }

        public SceneContext(IProjectContext fsContext, CameraController cameraController) {
            FSContext = fsContext;
            CameraController = cameraController;
            
            Content = new GameObject("[SceneContext]").transform;
            //UpdateManager = CreateMonoBehaviourInstance<UpdateManager>(Content);
            
            /*Systems = new Game.Systems.Systems(this);
            Systems.Register<UpdateCharacteristicsSystem>();
            Systems.Register<UpdateInteractionSystem>();
            Systems.Register<VillageInteractionSystem>();
            Systems.Register<VillageInteractionVisibility>();
            Systems.Register<AutoSaveSystem>();
            Systems.Register<QuestAutoActivationSystem>();
            Systems.Register<QuestProgressionSystem>();
            Systems.Register<CraftingSystem>();*/

#if UNITY_EDITOR
            if (CameraController != null) {
                //CameraController.Transform.parent = Content;
            }
#endif
        }
    }
}