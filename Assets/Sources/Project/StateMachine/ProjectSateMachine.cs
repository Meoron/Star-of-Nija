namespace Sources.Project.StateMachine{
	public sealed class ProjectSateMachine : Common.StateMachine.StateMachine{
		public IProjectController Controller { get;}
		public IProjectContext ProjectContext { get; }
	
		public ProjectSateMachine(IProjectController controller) : base(controller){
			Controller = controller;
			ProjectContext = Controller.ProjectContext;
		}
	}
}

