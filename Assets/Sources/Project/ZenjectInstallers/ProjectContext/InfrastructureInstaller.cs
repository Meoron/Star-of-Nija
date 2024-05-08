using Sources.Common.Input;
using Sources.Platforms;
using Sources.Project.Factories;
using Sources.Project.Managers;
using Sources.Project.Managers.UpdateManager;
using Sources.Project.StateMachine;
using UnityEngine;
using Zenject;

public sealed class InfrastructureInstaller : MonoInstaller{
	[SerializeField] private Transform _infrastructureTransfrom;
	
	public override void InstallBindings(){
		BindServiecs();
		BindFactories();
		BindManagers();
	}

	private void BindServiecs(){
		Container.BindInterfacesAndSelfTo<InputService>().AsSingle().WithArguments(_infrastructureTransfrom).NonLazy();
		Container.Bind<IPlatformServices>().To<PlatformServices>().AsSingle().NonLazy();
	}

	private void BindFactories(){
		Container.BindInterfacesAndSelfTo<ZenjectProjectStateFactory>().AsSingle();
		Container.BindInterfacesAndSelfTo<ZenjectWindowFactory>().AsSingle();
	}

	private void BindManagers(){
		var updateManager =
			CreateAndGetMonoBehaviorInstance<UpdateManager>("[UpdateManager]", _infrastructureTransfrom);
		
		Container.Bind<IUpdateManager>().FromInstance(updateManager).AsSingle().NonLazy();
		Container.Bind<IAccountManager>().To<AccountManager>().AsSingle().NonLazy();
		Container.BindInterfacesAndSelfTo<WindowManager>().AsSingle().NonLazy();
	}

	private T CreateAndGetMonoBehaviorInstance<T>(string name,Transform parent) where T : MonoBehaviour{
		var sceneObject = new GameObject(name);
		var component= sceneObject.AddComponent<T>();
		
		sceneObject.transform.SetParent(parent);
		
		return component;
	}
}