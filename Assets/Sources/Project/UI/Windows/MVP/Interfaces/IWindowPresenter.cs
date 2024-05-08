using System.Threading.Tasks;

namespace Sources.Project.UI.Windows{
	public interface IWindowPresenter{
		public void OnOpen();
		public void OnClose();
	}
}