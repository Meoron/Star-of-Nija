using System.Collections.Generic;
using Sources.Common.Services;
using Sources.Common.StateMachine;

namespace Sources.Project.StateMachine{
	public sealed class BootstrapProjectState : IState{
		private IStateMachine _projectStateMachine;
		private readonly List<IInitializable> _initializableServices;
		
		public BootstrapProjectState(ProjectStateMachine stateMachine, List<IInitializable> initializableServices){
			_projectStateMachine = stateMachine;
			_initializableServices = initializableServices;
		}
		
		public void Enter(){
			for (int i = 0; i < _initializableServices.Count; i++){
				_initializableServices[i].Initialize();
			}
			
			_projectStateMachine.EnterState<AuthenticationProjectState>();
		}

		public void Exit(){
		}
	}
}
