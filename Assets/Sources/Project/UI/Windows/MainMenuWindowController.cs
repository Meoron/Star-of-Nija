using System.Threading.Tasks;
using Sources.Project.Managers;
using Sources.Project.StateMachine;
using UnityEngine;

namespace Sources.Project.UI.Windows{
	public sealed class MainMenuWindowController : WindowController{
		private readonly ProjectStateMachine _projectStateMachine;
		
		public MainMenuWindowController(IWindowManager windowManager, ProjectStateMachine projectStateMachine){
			_windowManager = windowManager;
			_projectStateMachine = projectStateMachine;
		}
		
		public override async Task Initialize(Transform windowContainer){
			var windowView = await LoadAndGetView<MainMenuWindowView>("Prefabs/UI/Windows/Window - MainMenuView") as MainMenuWindowView;
			var windowModel = new MainMenuWindowModel(_windowManager, _projectStateMachine);
			
			windowView = GameObject.Instantiate(windowView, windowContainer);
			_windowPresenter = new MainMenuWindowPresenter(windowModel, windowView);
		}

		public override void OnOpen(){
			_windowPresenter.OnOpen();
		}

		public override void OnClose(){
			_windowPresenter.OnClose();
		}
	}
}