using Sources.Common.StateMachine;

public sealed class InitializationProjectState : ProjectState{
	public override void Initialize(StateMachine stateMachine){
		_stateMachine = stateMachine as ProjectSateMachine;
		
		ProjectContext.WindowManager.Initialize(ProjectContext);
	}
}
