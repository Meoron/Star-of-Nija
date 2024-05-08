using Sources.Project.Managers.UpdateManager;
using Zenject;

namespace Sources.Project.StateMachine{
	public sealed class ProjectStateMachine : Common.StateMachine.StateMachine, IInitializable, IUpdatable{
		private IUpdateManager _updateManager;
		
		public ProjectStateMachine(ZenjectProjectStateFactory zenjectProjectStatesFactory, IUpdateManager updateManager) : base(zenjectProjectStatesFactory){
			_updateManager = updateManager;
		}

		//This is entry point. Initialize start after bindings (Zenject interface)
		public void Initialize() { 
			_updateManager.Register(this);
			
			RegisterState<BootstrapProjectState>();
			RegisterState<AuthenticationProjectState>();
			RegisterState<LoadingProjectState>();
			RegisterState<LobbyProjectState>();
			RegisterState<GameLoopProjectState>();
			
			EnterState<BootstrapProjectState>();
		}

		public void OnUpdate(float deltaTime){
			UpdateStateLogic(deltaTime);
		}
	}
}

