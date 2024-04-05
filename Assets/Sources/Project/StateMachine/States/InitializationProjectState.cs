namespace Sources.Project.StateMachine{
	public sealed class InitializationProjectState : ProjectState{
		public override void Initialize(Common.StateMachine.StateMachine stateMachine){
			base.Initialize(stateMachine);
			
			ProjectContext.WindowManager.Initialize(ProjectContext);
			
			_stateMachine.ApplyState<AuthenticationProjectState>();
		}

		public override void Release(){
		}

		public override void OnUpdate(float deltaTime){
		}
	}
}
