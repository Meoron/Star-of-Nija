using Sources.Project.Managers;
using UnityEngine;

namespace Sources.Project.UI.Windows{
	public sealed class MainMenuWindowPresenter : IWindowPresenter{
		private MainMenuWindowModel _windowModel;
		private MainMenuWindowView _windowView;
		
		public MainMenuWindowPresenter(MainMenuWindowModel windowModel, MainMenuWindowView windowView){
			_windowModel = windowModel;
			_windowView = windowView;
		}

		public void Initialize(){
			
		}

		public void OnOpen(){
			_windowView.OnContinueClickButton += _windowModel.OnContinueClickButton;
			_windowView.OnNewGameClickButton += _windowModel.OnNewGameClickButton;
			_windowView.OnLoadGameClickButton += _windowModel.OnLoadGameClickButton;
			_windowView.OnSettingsClickButton += _windowModel.OnClickSettingsButton;
			_windowView.OnExitClickButton += _windowModel.OnClickExitButton;
		}

		public void OnClose(){
			_windowView.OnContinueClickButton -= _windowModel.OnContinueClickButton;
			_windowView.OnNewGameClickButton -= _windowModel.OnNewGameClickButton;
			_windowView.OnLoadGameClickButton -= _windowModel.OnLoadGameClickButton;
			_windowView.OnSettingsClickButton -= _windowModel.OnClickSettingsButton;
			_windowView.OnExitClickButton -= _windowModel.OnClickExitButton;
			
			GameObject.Destroy(_windowView);
		}
	}
}

