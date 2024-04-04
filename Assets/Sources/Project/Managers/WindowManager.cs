using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sources.Common.AssetManager;
using UnityEngine;

namespace Sources.Project.Managers {
    public interface IWindowManager {
        void Initialize(IProjectContext projectContext);
        void Release();
        
        IProjectContext FSContext { get; }
        
        WindowController Current { get; }
        
        bool IsBlockedInput { get; }
        
        Task<T> Open<T>(string path, bool autoInitialize = true) where T : WindowController;
        void Close<T>(T window) where T : WindowController;
        void CloseAll<T>() where T : WindowController;
        T Get<T>() where T : WindowController;
    }
    
    public sealed class WindowManager : MonoBehaviour, IWindowManager {
        [SerializeField] private WindowController[] _openedWindows = new WindowController[0];

        private Dictionary<LayoutLevel, Transform> _canvasMap = new Dictionary<LayoutLevel, Transform>();
        
        public IProjectContext FSContext { get; private set; }
        public WindowController Current { get; private set; }
        
        public bool IsBlockedInput { get; private set; }

        public void Initialize(IProjectContext projectContext) {
            FSContext = projectContext;
        }

        public void Release() {
        }
        
        public async Task<T> Open<T>(string path, bool autoInitialize = true) where T : WindowController {
            var window = Get<T>();
            if (window != null) return window;
            
            var prefab = await AssetManager.LoadPrefabAsync<T>(path);
            var layout = prefab.GetLayout();
            if (!_canvasMap.ContainsKey(layout)) {
                var container = new GameObject($"{layout}").transform;
                container.transform.parent = transform;
                _canvasMap.Add(layout, container);
            }
            
            window = Instantiate(prefab, _canvasMap[layout]);
            window.GetComponent<Canvas>().sortingOrder = (int)layout * 1000 + _canvasMap[layout].childCount;
            
            if (autoInitialize) {
                window.Initialize(this);
            }
            
            _openedWindows = _openedWindows.Append(window).ToArray();

            Current?.OnFocusChanged(false);
            Current = window;
            window.OnFocusChanged(true);
           
            return window;
        }

        public void Close<T>(T window) where T : WindowController {
            if (window == null) return;
            
            window.Release();
            _openedWindows = _openedWindows.Where(x => x != window).ToArray();
            DestroyImmediate(window.gameObject);
            Current = _openedWindows.LastOrDefault();
            Current?.OnFocusChanged(true);
        }
        
        public void CloseAll<T>() where T : WindowController {
            var windows = _openedWindows.Where(x => x is T).ToArray();
            foreach (var window in windows) {
                Close(window as T);
            }
        }

        public T Get<T>() where T : WindowController {
            return _openedWindows.FirstOrDefault(x => x is T) as T;
        }

        private void Update() {
            foreach (var window in _openedWindows) {
                window.OnUpdate();
            }
        }
        

        private void FixedUpdate() {
            foreach (var window in _openedWindows) {
                window.OnFixedUpdate();
            }
        }

        /*private void LateUpdate() {
            IsBlockedInput = false;
            var inputWindow = (WindowControllerWithJoystick) null;
            foreach (var window in _openedWindows) {
                if (window is WindowControllerWithJoystick) {
                    inputWindow = window as WindowControllerWithJoystick;
                }
                window.OnDrawUpdate();
            }

            IsBlockedInput = inputWindow != null && inputWindow.IsPlayerInputBlocked;
            inputWindow?.OnInputUpdate();
        }*/
    }

    public enum LayoutLevel {
        HUD,
        Subtitles,
        Window,
        Dialogue,
        Dialog,
        Tutorial,
        Pause,
        Notifications,
        Warning,
        Overlay
    }
}