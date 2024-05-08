namespace Sources.Project.UI.Windows{
	public interface IWindowView{
		public void Initialize();
		public void OnOpen();
		public void OnClose();
	}
}