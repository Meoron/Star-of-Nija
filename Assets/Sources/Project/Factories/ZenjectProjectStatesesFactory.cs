using Sources.Common.StateMachine;
using Zenject;

namespace Sources.Project.StateMachine{
	public sealed class ZenjectProjectStateFactory : IStateFactory{
		private readonly DiContainer _container;

		public ZenjectProjectStateFactory(DiContainer container){
			_container = container;
		}
	
		public T CreateState<T>() where T : IExitableState{
			_container.Bind<T>().AsSingle().NonLazy();
			return _container.Resolve<T>();
		}
	}
}

