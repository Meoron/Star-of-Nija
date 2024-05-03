using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Sources.Project.Managers.UpdateManager{
	public sealed class UpdateManager : MonoBehaviour, IUpdateManager{
		private static UpdateManager _instance;
		private readonly List<IUpdatable> _updatableObjects = new List<IUpdatable>();
		private readonly List<IFixedUpdatable> _fixedUpdatableObjects = new List<IFixedUpdatable>();
		private readonly List<ILateUpdatable> _lateUpdatableObjects = new List<ILateUpdatable>();

		public bool HasRegisteredObjects => _updatableObjects.Count > 0
											|| _lateUpdatableObjects.Count > 0
											|| _fixedUpdatableObjects.Count > 0;

		public static UpdateManager Instance => _instance;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Register(IManagedObject monoUpdatable){
			if (monoUpdatable is IUpdatable updatable){
				_updatableObjects.Add(updatable);
			}

			if (monoUpdatable is IFixedUpdatable fixedUpdatable){
				_fixedUpdatableObjects.Add(fixedUpdatable);
			}

			if (monoUpdatable is ILateUpdatable lateUpdatable){
				_lateUpdatableObjects.Add(lateUpdatable);
			}

			enabled = HasRegisteredObjects;
			Debug.Log(monoUpdatable);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Unregister(IManagedObject monoUpdatable){
			if (monoUpdatable is IUpdatable updatable){
				_updatableObjects.Remove(updatable);
			}

			if (monoUpdatable is IFixedUpdatable fixedUpdatable){
				_fixedUpdatableObjects.Remove(fixedUpdatable);
			}

			if (monoUpdatable is ILateUpdatable lateUpdatable){
				_lateUpdatableObjects.Remove(lateUpdatable);
			}
			
			enabled = HasRegisteredObjects;
		}

		public void Clear()
		{
			_updatableObjects.Clear();
			_lateUpdatableObjects.Clear();
			_fixedUpdatableObjects.Clear();
			enabled = false;
		}
		
		private void Awake(){
			_instance = this;
		}

		private void Update(){
			var count = _updatableObjects.Count;
			for (int i = 0; i < count; i++){
				try{
					_updatableObjects[i].OnUpdate(Time.deltaTime);
				}
				catch (Exception ex){
					Debug.LogException(ex);
				}
			}
		}

		private void FixedUpdate(){
			var count = _fixedUpdatableObjects.Count;
			for (int i = 0; i < count; i++){
				try{
					_fixedUpdatableObjects[i].OnFixedUpdate(Time.deltaTime);
				}
				catch (Exception ex){
					Debug.LogException(ex);
				}
			}
		}

		private void LateUpdate(){
			var count = _fixedUpdatableObjects.Count;
			for (int i = 0; i < count; i++){
				try{
					_lateUpdatableObjects[i].OnLateUpdate(Time.deltaTime);
				}
				catch (Exception ex){
					Debug.LogException(ex);
				}
			}
		}
	}
}