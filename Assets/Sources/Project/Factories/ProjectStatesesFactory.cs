using Sources.Common.StateMachine;
using Zenject;

namespace Sources.Project.StateMachine{
	public sealed class ProjectStateFactory : IStateFactory{
		private readonly DiContainer _container;

		public ProjectStateFactory(DiContainer container){
			_container = container;
		}
	
		public T CreateState<T>() where T : IExitableState{
			return _container.Resolve<T>();
		}
	}
}

