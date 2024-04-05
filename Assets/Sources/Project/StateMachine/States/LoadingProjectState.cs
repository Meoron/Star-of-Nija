using System;
using System.Threading.Tasks;
using Sources.Common.StateMachine;
using Sources.Project.Scenes;
using Sources.Project.StateMachine;
using Sources.Project.UI.Windows;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class LoadingProjectState : ProjectState{
	public override async void Initialize(StateMachine stateMachine) {
		base.Initialize(stateMachine);
		
		var loaderWindow = await ProjectContext.WindowManager.Open<LoadingWindowController>("Prefabs/UI/Window - Loading");
		while (loaderWindow.IsInProgress) {
			await Task.Delay(100);
		}
            
		await Task.Delay(1000);

		//var user = ProjectContext.PlatformContext.UserService.GetPrimaryUser();
		//var 
		var sceneName = "Test";
		//var sceneName = ProjectContext.PlatformContext.UserService.GetPrimaryUser().Progress.LastLocation;
		/*if (string.IsNullOrEmpty(sceneName)) {
			sceneName = GameConfig.DefaultSceneName;
		}*/
            
		await LoadScene(sceneName);
		await InitializeScene(sceneName);
		await UnloadAndCleanMemory();

		loaderWindow.Close();

		_stateMachine.ApplyState<GameProjectState>();
	}

	public override void Release(){
		throw new System.NotImplementedException();
	}

	public override void OnUpdate(float deltaTime){
		throw new System.NotImplementedException();
	}

	private async Task LoadScene(string sceneName) {
		var asyncOp = SceneManager.LoadSceneAsync(sceneName);
		while (!asyncOp.isDone) {
			await Task.Delay(100);
		}
	}

	private async Task InitializeScene(string sceneName) {
		var sceneController = MonoBehaviour.FindObjectOfType<SceneController>();
		await sceneController.Initialize(ProjectContext, sceneName);
	}

	private async Task UnloadAndCleanMemory() {
		Resources.UnloadUnusedAssets();
		await Task.Delay(100);
            
		GC.Collect();
		await Task.Delay(100);
	}
}