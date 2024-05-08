using System.Threading.Tasks;
using Sources.Common.AssetManager;
using Sources.Project.Managers;
using UnityEngine;

namespace Sources.Project.UI.Windows{
	public abstract class WindowController : IWindowController{
		protected IWindowManager _windowManager;
		protected IWindowPresenter _windowPresenter; 
		
		public async virtual Task Initialize(Transform windowContainer){ 
		}
		
		public virtual void OnOpen(){
			_windowPresenter.OnOpen();
		}
		
		public virtual void OnClose(){
			_windowPresenter.OnClose();
		}
		
		protected async Task<IWindowView> LoadAndGetView<T>(string path) where T : MonoBehaviour{ 
			var windowViewPrefab = await AssetManager.LoadPrefabAsync<T>(path);
			var windowView = GameObject.Instantiate(windowViewPrefab).GetComponent<IWindowView>();
			return windowView;
		}
	}
}


