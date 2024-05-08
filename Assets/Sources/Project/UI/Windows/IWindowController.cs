using System.Threading.Tasks;
using UnityEngine;

namespace Sources.Project.UI.Windows{
	public interface IWindowController{
		//Transform of WindowContainer need for WindowView
		public Task Initialize(Transform windowContainer);
		public void OnOpen();
		public void OnClose();
	}
}

