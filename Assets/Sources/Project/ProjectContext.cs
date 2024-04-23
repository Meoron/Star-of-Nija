
using Sources.Common.Context;

namespace Sources.Project{
	public interface IProjectContext{
		
	}
	public sealed class ProjectContext : Context, IProjectContext{
		public ProjectContext(){
			
		}
	}
}