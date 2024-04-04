using Sources.Common.Context;
using Sources.Common.Input;
using Sources.Platform;
using Sources.Project.Managers;
using UnityEngine;

namespace Sources.Project{
	public interface IProjectContext{
		public IPlatformContext PlatformContext { get; }
		public IInputManager InputManager { get; }
		public IProjectController ProjectController{ get; }
		public IWindowManager WindowManager { get; }
	}
	public sealed class ProjectContext : Context, IProjectContext{
		public IPlatformContext PlatformContext { get; }
		public IProjectController ProjectController{ get; }
		public IInputManager InputManager { get; }
		public IWindowManager WindowManager { get; }

		public ProjectContext(IProjectController projectController, Transform parent){
			PlatformContext = new PlatformContext();
			InputManager = new InputManager(parent);
			WindowManager = new WindowManager();
			
			ProjectController = projectController;
			
			WindowManager = CreateMonoBehaviourInstance<WindowManager>(parent);
		}
	}
}