using Zenject;

namespace Sources.Project.StateMachine{
	public sealed class ProjectStateMachineInstaller : MonoInstaller{
		public override void InstallBindings(){
			Container.Bind<BootstrapProjectState>().AsSingle().NonLazy();
			Container.Bind<AuthenticationProjectState>().AsSingle().NonLazy();
			Container.Bind<LobbyProjectState>().AsSingle().NonLazy();
			Container.Bind<LoadingProjectState>().AsSingle().NonLazy();
			Container.Bind<GameLoopProjectState>().AsSingle().NonLazy();
        
			Container.BindInterfacesAndSelfTo<ProjectSateMachine>().AsSingle();  //(Entry point) Initialized after bindings
		}
	}   
}