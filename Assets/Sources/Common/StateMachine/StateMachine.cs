
using UnityEngine;

namespace Sources.Common.StateMachine {
    public interface IStateMachine : IUpdateable {
        public IController Controller { get; }
        public State CurrentState { get; }
        public State PreviousState { get; }
        
        void Initialize();
        void Release();
        void ApplyState<T>() where T : State, new();
    }
    
    public class StateMachine : IStateMachine {
        public IController Controller { get; }
        public State CurrentState { get; private set; }
        public State PreviousState { get; private set; }

        public StateMachine(IController controller) {
            Controller = controller;
        }
        
        public virtual void Initialize() {
        }

        public virtual void Release() {
            if (CurrentState != null) {
                CurrentState.Release();
            }
        }

        public void ApplyState<T>() where T : State, new() {
            if (CurrentState != null) {
                PreviousState = CurrentState;
                CurrentState.Release();
            }

            Debug.Log($"Player state is changed. From [{CurrentState?.GetType().Name}] to [{typeof(T).Name}]");
            
            CurrentState = new T();
            CurrentState.Initialize(this);
        }

        public void OnUpdate(float deltaTime) {
            CurrentState?.OnUpdate(deltaTime);
        }
    }
}