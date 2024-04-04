using Sources.Common.StateMachine;
using Sources.Project;

public sealed class ProjectSateMachine : StateMachine{
	public IProjectController Controller { get;}
	public IProjectContext ProjectContext { get; }
	
	public ProjectSateMachine(IProjectController controller) : base(controller){
		Controller = controller;
		ProjectContext = Controller.ProjectContext;
	}
}
