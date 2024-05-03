using Sources.Project.Managers.UpdateManager;
using Zenject;

namespace Sources.Project.StateMachine{
	public sealed class ProjectSateMachine : Common.StateMachine.StateMachine, IInitializable, IUpdatable{
		private IUpdateManager _updateManager;
		
		public ProjectSateMachine(ProjectStateFactory projectStatesFactory,  IUpdateManager updateManager) : base(projectStatesFactory){
			_updateManager = updateManager;
		}

		//This is entry point. Initialize start after bindings (Zenject interface)
		public void Initialize() { 
			_updateManager.Register(this);
			
			RegisterState<BootstrapProjectState>();
			RegisterState<AuthenticationProjectState>();
			RegisterState<LoadingProjectState>();
			
			EnterState<BootstrapProjectState>();
		}

		public void OnUpdate(float deltaTime){
			UpdateStateLogic(deltaTime);
		}
	}
}

