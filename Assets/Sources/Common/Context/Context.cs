using UnityEngine;

namespace Sources.Common.Context {
    public interface IContext {
        void Dispose();
    }
    
    public class Context : IContext {
        protected T CreateInstanceFromResources<T>(Transform parent, string path) where T : MonoBehaviour {
            var prefab = Resources.Load<T>(path);
            if (prefab == null) {
                throw new System.Exception($"Can't load {typeof(T)} manager by [{path}] path");
            }
            var instance = MonoBehaviour.Instantiate(prefab, parent);
            instance.gameObject.name = $"[{instance.gameObject.name}]";
            return instance;
        }
        
        protected T CreateMonoBehaviourInstance<T>(Transform parent) where T : MonoBehaviour {
            var instance = new GameObject($"[{typeof(T).Name}]").AddComponent<T>();
            instance.transform.parent = parent;
            return instance;
        }
        
        protected T CreateInstance<T>() where T : new() {
            return new T();
        }

        public virtual void Dispose() {
        }
    }
}