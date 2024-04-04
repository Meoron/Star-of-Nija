using UnityEngine;

namespace Sources.Common.PoolManager {
    public interface IPoolManager {
        T Spawn<T>(Vector3 position, Quaternion rotation) where T : IPoolEntity;
        T Spawn<T>(Vector3 position, Quaternion rotation, Transform parent) where T : IPoolEntity;
    }
    
    public sealed class PoolManager : MonoBehaviour, IPoolManager {
        public T Spawn<T>(Vector3 position, Quaternion rotation) where T : IPoolEntity {
            return Spawn<T>(position, rotation, null);
        }

        public T Spawn<T>(Vector3 position, Quaternion rotation, Transform parent) where T : IPoolEntity {
            return default;
        }
    }
}