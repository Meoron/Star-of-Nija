using Sources.Common.Input;
using Sources.Platforms;
using Sources.Project.Managers;
using Sources.Project.Managers.UpdateManager;
using Sources.Project.StateMachine;
using UnityEngine;
using Zenject;

public sealed class InfrastructureInstaller : MonoInstaller{
	[SerializeField] private Transform _infrastructureTransfrom;
	
	public override void InstallBindings(){
		BindServiecs();
		BindManagers();
		BindFactories();
	}

	private void BindServiecs(){
		Container.Bind<IInputService>().To<InputService>().AsSingle().WithArguments(_infrastructureTransfrom).NonLazy();
		Container.Bind<IPlatformServices>().To<PlatformServices>().AsSingle().NonLazy();
	}

	private void BindFactories(){
		Container.BindInterfacesAndSelfTo<ProjectStateFactory>().AsSingle();
	}

	private void BindManagers(){
		var updateManager =
			CreateAndGetMonoBehaviorInstance<UpdateManager>("[UpdateManager]", _infrastructureTransfrom);
		var windowManager =
			CreateAndGetMonoBehaviorInstance<WindowManager>("[WindowManager]", _infrastructureTransfrom);
		
		Container.Bind<IUpdateManager>().FromInstance(updateManager).AsSingle().NonLazy();
		Container.Bind<IWindowManager>().FromInstance(windowManager).AsSingle().NonLazy();
		Container.Bind<IAccountManager>().To<AccountManager>().AsSingle().NonLazy();
		
	}

	private T CreateAndGetMonoBehaviorInstance<T>(string name,Transform parent) where T : MonoBehaviour{
		var sceneObject = new GameObject(name);
		var component= sceneObject.AddComponent<T>();
		
		sceneObject.transform.SetParent(parent);
		
		return component;
	}
}