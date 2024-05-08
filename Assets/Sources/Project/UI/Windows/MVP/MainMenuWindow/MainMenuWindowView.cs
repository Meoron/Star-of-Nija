using System;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Project.UI.Windows{
	public class MainMenuWindowView : MonoBehaviour, IWindowView{
		[SerializeField] private Button _continueButton;
		[SerializeField] private Button _newGameButton;
		[SerializeField] private Button _loadGameButton;
		[SerializeField] private Button _settingsButton;
		[SerializeField] private Button _exitButton;

		public event Action OnContinueClickButton;
		public event Action OnNewGameClickButton;
		public event Action OnLoadGameClickButton;
		public event Action OnSettingsClickButton;
		public event Action OnExitClickButton;
		
		public void Initialize(){
			
		}

		public void OnOpen(){
			_continueButton.onClick.AddListener(()=> OnContinueClickButton?.Invoke());
			_newGameButton.onClick.AddListener(()=> OnNewGameClickButton?.Invoke());
			_loadGameButton.onClick.AddListener(()=> OnLoadGameClickButton?.Invoke());
			_settingsButton.onClick.AddListener(()=> OnSettingsClickButton?.Invoke());
			_exitButton.onClick.AddListener(()=> OnExitClickButton?.Invoke());
		}

		public void OnClose(){
			_continueButton.onClick.RemoveAllListeners();
			_newGameButton.onClick.RemoveAllListeners();
			_loadGameButton.onClick.RemoveAllListeners();
			_settingsButton.onClick.RemoveAllListeners();
			_exitButton.onClick.RemoveAllListeners();
		}
	}
}