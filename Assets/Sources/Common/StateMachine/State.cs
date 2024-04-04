namespace Sources.Common.StateMachine {
    public abstract class State: IUpdateable{
        protected StateMachine _stateMachine;

        public abstract void Initialize(StateMachine stateMachine);
        public abstract void Release();

        public abstract void OnUpdate(float deltaTime);
    }
}