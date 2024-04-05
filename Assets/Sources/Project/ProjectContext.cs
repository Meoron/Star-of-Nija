using Sources.Common.Context;
using Sources.Common.Input;
using Sources.Platforms;
using Sources.Project.Managers;
using UnityEngine;

namespace Sources.Project{
	public interface IProjectContext{
		public IPlatformContext PlatformContext { get; }
		public IInputManager InputManager { get; }
		public IAccountManager AccountManager{ get; }
		public IProjectController ProjectController{ get; }
		public IWindowManager WindowManager { get; }
	}
	public sealed class ProjectContext : Context, IProjectContext{
		public IPlatformContext PlatformContext { get; }
		public IInputManager InputManager { get; }
		public IAccountManager AccountManager{ get; }
		public IProjectController ProjectController{ get; }
		
		public IWindowManager WindowManager { get; }

		public ProjectContext(IProjectController projectController, Transform parent){
			PlatformContext = new PlatformContext();
			InputManager = new InputManager(parent);
			AccountManager = new AccountManager();
			WindowManager = new WindowManager();
			
			ProjectController = projectController;
			
			WindowManager = CreateMonoBehaviourInstance<WindowManager>(parent);
		}
	}
}