namespace Sources.Common.StateMachine{
	public interface IUpdatableState{
		public void OnUpdate(float deltaTime);
	}

	public interface IState : IExitableState{
		public void Enter();
	}

	public interface IExitableState{
		public void Exit();
	}
}