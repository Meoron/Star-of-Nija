using System.Threading.Tasks;
using Sources.Project.Managers;
using UnityEngine;

namespace Sources.Project.UI.Windows{
	public class LoadingWindowController : IWindowController{
		private IWindowManager _windowManager;
		//private LoadingWindowPresenter _loadingWindowController;
		
		
		public LoadingWindowController(IWindowManager windowManager){
			_windowManager = windowManager;
			//_loadingWindowController = new LoadingWindowPresenter(_windowManager);
		}
		
		public async Task Initialize(Transform windowContainer){
		}

		public void OnOpen(){
		}

		public void OnClose(){
		}
	}
}