using Sources.Common.StateMachine;
using Sources.Project;

public class ProjectState : State{
	protected ProjectSateMachine _stateMachine;
	protected IProjectContext ProjectContext { get { return _stateMachine.ProjectContext; } }
	
	public override void Initialize(StateMachine stateMachine){
		_stateMachine = stateMachine as ProjectSateMachine;
	}

	public override void Release(){
	}

	public override void OnUpdate(float deltaTime){
	}
}
