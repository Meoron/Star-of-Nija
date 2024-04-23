using Sources.Common.StateMachine;
using UnityEngine;

namespace Sources.Project.StateMachine{
	public sealed class BootstrapProjectState : IState{
		
		public BootstrapProjectState(ProjectSateMachine stateMachine){
			
		}
		
		public void Enter(){
			Debug.Log("Bootstrap entered");
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
