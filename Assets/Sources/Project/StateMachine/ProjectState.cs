using Sources.Common.StateMachine;
using Sources.Project;
using Sources.Project.StateMachine;

public abstract class ProjectState : State{
	protected new ProjectSateMachine _stateMachine;
	protected IProjectContext ProjectContext { get { return _stateMachine.ProjectContext; } }
	
	public override void Initialize(StateMachine stateMachine) {
		_stateMachine = stateMachine as ProjectSateMachine;
	}
}
