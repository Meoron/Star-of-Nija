using System.Collections.Generic;
using Sources.Common.Services;
using Sources.Common.StateMachine;

namespace Sources.Project.StateMachine{
	public sealed class BootstrapProjectState : IState{
		private readonly List<IInitializable> _initializableServices;
		
		public BootstrapProjectState(ProjectSateMachine stateMachine, List<IInitializable> initializableServices){
			_initializableServices = initializableServices;
		}
		
		public void Enter(){
			for (int i = 0; i < _initializableServices.Count; i++){
				_initializableServices[i].Initialize();
			}
			
			//var parentTransform = _stateMachine.Controller.transform;
			/*var windowManager = CreateMonoBehaviourInstance<WindowManager>(parentTransform);
			
			_projectDiContainer.RegisterSingle<IPlatformContext, PlatformContext>();
			_projectDiContainer.RegisterSingleWithArguments<IInputManager, InputManager>(parentTransform);
			_projectDiContainer.RegisterSingle<IAccountManager, AccountManager>();
			_projectDiContainer.RegisterSingleFormPrefab<IWindowManager, WindowManager>(windowManager);
			
			_stateMachine.EnterState<AuthenticationProjectState>();*/
		}

		public void Exit(){
		}

		/*public T CreateInstanceFromResources<T>(Transform parent, string path) where T : MonoBehaviour {
			var prefab = Resources.Load<T>(path);
			if (prefab == null) {
				throw new System.Exception($"Can't load {typeof(T)} manager by [{path}] path");
			}
			var instance = MonoBehaviour.Instantiate(prefab, parent);
			instance.gameObject.name = $"[{instance.gameObject.name}]";
			return instance;
		}*/
        
		/*public T CreateMonoBehaviourInstance<T>(Transform parent) where T : MonoBehaviour {
			var instance = new GameObject($"[{typeof(T).Name}]").AddComponent<T>();
			instance.transform.parent = parent;
			return instance;
		}*/
	}
}
