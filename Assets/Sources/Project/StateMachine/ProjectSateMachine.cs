using Zenject;

namespace Sources.Project.StateMachine{
	public sealed class ProjectSateMachine : Common.StateMachine.StateMachine, IInitializable{
		public ProjectSateMachine(ProjectStateFactory projectStatesFactory) : base(projectStatesFactory){
			//var authenticationState = new AuthenticationProjectState(this);
		}

		public void Initialize(){
			RegisterState<BootstrapProjectState>();
			RegisterState<AuthenticationProjectState>();
			RegisterState<LoadingProjectState>();
			
			EnterState<BootstrapProjectState>();
		}
	}
}

