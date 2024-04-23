namespace Sources.Common.StateMachine{
	public interface IUpdatableState : IUpdateable{
	}

	public interface IState : IExitableState{
		public void Enter();
	}

	public interface IExitableState{
		public void Exit();
	}
}