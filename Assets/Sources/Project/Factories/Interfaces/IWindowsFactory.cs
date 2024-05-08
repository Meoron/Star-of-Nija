using Sources.Project.UI.Windows;

namespace Sources.Project.Factories{
	public interface IWindowsFactory{
		public T CreateWindow<T>() where T : IWindowController;
	}
}