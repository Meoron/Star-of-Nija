using System.Threading.Tasks;
using Sources.Common.StateMachine;
using Sources.Project.Managers;
using Sources.Project.UI.Windows;
using UnityEngine.SceneManagement;

namespace Sources.Project.StateMachine{
	public sealed class LobbyProjectState : IState{
		private IStateMachine _projectStateMachine;
		private IWindowManager _windowManager;
		
		public LobbyProjectState(ProjectStateMachine stateMachine, IWindowManager windowManager){
			_projectStateMachine = stateMachine;
			_windowManager = windowManager;
		}
		
		public async void Enter(){
			await LoadScene("Lobby");
			
			_windowManager.Open<MainMenuWindowController>("Prefabs/UI/Window - MainMenuView");
		}

		public void Exit(){
		}
		
		private async Task LoadScene(string sceneName) {
			var asyncOp = SceneManager.LoadSceneAsync(sceneName);
			while (!asyncOp.isDone) {
				await Task.Delay(100);
			}
		}
	}
}