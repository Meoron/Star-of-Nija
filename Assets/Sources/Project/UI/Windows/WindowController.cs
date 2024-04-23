using System;
using Sources.Project.Managers;
using UnityEngine;

namespace Sources.Project {
    public abstract class WindowController : MonoBehaviour {
        [SerializeField] protected RectTransform _contentRect;
        [SerializeField] protected RectTransform _hintContainer;
        
        protected IWindowManager _windowManager;

        public event Action OnClose;
        public bool IsFocused;
        
        public void Initialize(IWindowManager windowManager) {
            _windowManager = windowManager;
            OnInitialize();
        }

        public void Release() {
            OnRelease();
        }

        public virtual void Close() {
            OnClose?.Invoke();
            _windowManager.Close(this);
        }
        
        public abstract LayoutLevel GetLayout();
        protected abstract void OnInitialize();
        protected abstract void OnRelease();
        public abstract void OnDrawUpdate();
        public abstract void OnUpdate();
        public abstract void OnFixedUpdate();

        public virtual void OnFocusChanged(bool isFocus) {
            IsFocused = isFocus;
            if (_hintContainer != null) {
                _hintContainer.gameObject.SetActive(isFocus);
            }
        }
    }
}