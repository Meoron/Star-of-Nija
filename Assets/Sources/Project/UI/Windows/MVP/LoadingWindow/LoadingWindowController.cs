using Sources.Project.Managers;

namespace Sources.Project.UI.Windows{
	public sealed class LoadingWindowPresenter : IWindowPresenter{
		private readonly IWindowManager _windowManager;
		private IWindowView _windowView;
		private IWindowModel _windowModel;
		
		public LoadingWindowPresenter(IWindowManager windowManager, IWindowView windowView, IWindowModel windowModel){
			_windowManager = windowManager;
			_windowModel = windowModel;
			_windowView = windowView;
		}

		public void Initialize(){
		}

		public void OnOpen(){
		}

		public void OnClose(){
		}
	}
}