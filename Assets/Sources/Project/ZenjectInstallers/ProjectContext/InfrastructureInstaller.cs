using Sources.Common.Input;
using Sources.Platforms;
using Sources.Project.Managers;
using Sources.Project.StateMachine;
using UnityEngine;
using Zenject;

public class InfrastructureInstaller : MonoInstaller{
	[SerializeField] private Transform _infrastructureTransfrom;
	
	public override void InstallBindings(){
		BindServiecs();
		BindFactories();
		BindManagers();
	}

	private void BindServiecs(){
		Container.Bind<IInputService>().To<InputService>().AsSingle().WithArguments(_infrastructureTransfrom).NonLazy();
		Container.Bind<IPlatformServices>().To<PlatformServices>().AsSingle().NonLazy();
	}

	private void BindFactories(){
		Container.BindInterfacesAndSelfTo<ProjectStateFactory>().AsSingle();
	}

	private void BindManagers(){
		var windowManager =
			CreateAndGetMonoBehaviorInstance<WindowManager>("[WindowManager]", _infrastructureTransfrom);
		
		Container.Bind<IAccountManager>().To<AccountManager>().AsSingle().NonLazy();
		Container.Bind<IWindowManager>().FromInstance(windowManager).AsSingle().NonLazy();
	}

	private T CreateAndGetMonoBehaviorInstance<T>(string name,Transform parent) where T : MonoBehaviour{
		var sceneObject = new GameObject(name);
		var component= sceneObject.AddComponent<T>();
		
		sceneObject.transform.SetParent(parent);
		
		return component;
	}
}