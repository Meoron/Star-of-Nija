namespace Sources.Project.StateMachine{
	public sealed class LobbyProjectState : ProjectState{
		public override void Initialize(Common.StateMachine.StateMachine stateMachine){
			base.Initialize(stateMachine);
			
			_stateMachine.ApplyState<LoadingProjectState>();
		}

		public override void Release(){
		}

		public override void OnUpdate(float deltaTime){
		}
	}
}