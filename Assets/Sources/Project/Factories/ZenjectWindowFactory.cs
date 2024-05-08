using Sources.Project.UI.Windows;
using Zenject;

namespace Sources.Project.Factories{
	public sealed class ZenjectWindowFactory : IWindowsFactory{
		private readonly DiContainer _container;

		public ZenjectWindowFactory(DiContainer container){
			_container = container;
		}

		public T CreateWindow<T>() where T : IWindowController{
			_container.Bind<T>().AsSingle();
			return _container.Resolve<T>();
		}
	}
}