using System;
using System.Threading.Tasks;
using Sources.Common.StateMachine;
using Sources.Project.Managers;
using Sources.Project.StateMachine;
using Sources.Project.UI.Windows;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class LoadingProjectState : IState{
	private IWindowManager _windowManger;
	private ProjectStateMachine _stateMachine;
	
	public LoadingProjectState(IWindowManager windowManager, ProjectStateMachine stateMachine){
		_windowManger = windowManager;
		_stateMachine = stateMachine;
	}
	
	public async void Enter() {
		var loaderWindow = await _windowManger.Open<LoadingWindowController>("Prefabs/UI/Window - Loading");
		/*while (loaderWindow.IsInProgress) {
			await Task.Delay(100);
		}*/
            
		await Task.Delay(1000);

		//var user = ProjectContext.PlatformContext.UserService.GetPrimaryUser();
		//var 
		var sceneName = "Test";
		//var sceneName = ProjectContext.PlatformContext.UserService.GetPrimaryUser().Progress.LastLocation;
		/*if (string.IsNullOrEmpty(sceneName)) {
			sceneName = GameConfig.DefaultSceneName;
		}*/
		
		await UnloadAndCleanMemory();
		await LoadScene(sceneName);
		//await InitializeScene(sceneName);
		

		loaderWindow.OnClose();

		_stateMachine.EnterState<GameLoopProjectState>();
	}

	public void Exit(){
		
	}

	private async Task LoadScene(string sceneName) {
		var asyncOp = SceneManager.LoadSceneAsync(sceneName);
		while (!asyncOp.isDone) {
			await Task.Delay(100);
		}
	}

	/*private async Task InitializeScene(string sceneName) {
		var sceneController = MonoBehaviour.FindObjectOfType<SceneController>();
		await sceneController.Initialize(ProjectContext, sceneName);
	}*/

	private async Task UnloadAndCleanMemory() {
		Resources.UnloadUnusedAssets();
		await Task.Delay(100);
            
		GC.Collect();
		await Task.Delay(100);
	}
}