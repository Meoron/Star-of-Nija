using Sources.Common.StateMachine;
using UnityEngine;

namespace Sources.Project.StateMachine{
	public sealed class LobbyProjectState : IState{

		private IStateMachine _projectStateMachine;
		public LobbyProjectState(ProjectSateMachine stateMachine){
			_projectStateMachine = stateMachine;
		}
		
		public void Enter(){
			Debug.Log("LobbyProjectState");
		}

		public void Exit(){
		}
	}
}