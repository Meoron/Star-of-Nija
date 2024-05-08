using Sources.Project.Managers;
using Sources.Project.StateMachine;

namespace Sources.Project.UI.Windows{
	public sealed class MainMenuWindowModel : IWindowModel{
		private IWindowManager _windowManager;
		private ProjectStateMachine _projectStateMachine;

		public MainMenuWindowModel(IWindowManager windowManager, ProjectStateMachine projectStateMachine){
			_windowManager = windowManager;
			_projectStateMachine = projectStateMachine;
		}
		
		public void OnContinueClickButton(){
			_projectStateMachine.EnterState<LoadingProjectState>();
		}
		
		public void OnNewGameClickButton(){
			
		}
		
		public void OnLoadGameClickButton(){
			
		}
		
		public void OnClickSettingsButton(){
			
		}
		
		public void OnClickExitButton(){
			
		}
	}
}

