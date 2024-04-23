using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sources.Common.StateMachine{
	public interface IStateMachine : IUpdateable{
		public void RegisterState<TState>() where TState : IExitableState;
		void EnterState<TState>() where TState : class, IState;
	}

	public class StateMachine : IStateMachine{
		public Dictionary<Type, IExitableState> _states;

		private readonly IStateFactory _stateFactory;
		private IUpdatableState _updatableState;
		private IExitableState _activeState;

		public StateMachine(IStateFactory stateFactory){
			_stateFactory = stateFactory;
		}

		public void RegisterState<TState>() where TState : IExitableState{
			_states ??= new Dictionary<Type, IExitableState>();
			_states[typeof(TState)] = _stateFactory.CreateState<TState>();
		}

		public void EnterState<TState>() where TState : class, IState{
			IState _activeState = ChangeState<TState>();
			_activeState.Enter();

			Debug.Log($"Player state is changed. From [{_activeState?.GetType().Name}] to [{typeof(TState).Name}]");
		}

		public void OnUpdate(float deltaTime){
			_updatableState?.OnUpdate(deltaTime);
		}

		private TState ChangeState<TState>() where TState : class, IExitableState{
			_activeState?.Exit();

			TState state = GetState<TState>();
			_updatableState = state as IUpdatableState;
			_activeState = state;

			return state;
		}

		private TState GetState<TState>() where TState : class, IExitableState{
			return _states[typeof(TState)] as TState;
		}
	}
}