using System.Linq;
using System.Threading.Tasks;
using Sources.Common.Services;
using Sources.Project.Factories;
using Sources.Project.Managers.UpdateManager;
using Sources.Project.UI.Windows;
using UnityEngine;

namespace Sources.Project.Managers {
    public interface IWindowManager {
        IWindowController Current { get; }
        bool IsBlockedInput { get; }
        
        Task<T> Open<T>(string path, bool autoInitialize = true) where T : class, IWindowController;
        void Close<T>(T window) where T : class, IWindowController;
        void CloseAll<T>() where T : class, IWindowController;
        T Get<T>() where T : class, IWindowController;
    }
    
    public sealed class WindowManager : IWindowManager, IInitializable, IUpdatable, IFixedUpdatable{
        private readonly ZenjectWindowFactory _zenjectWindowFactory;
        private readonly IUpdateManager _updateManager;
        
        private IWindowController[] _openedWindows = new IWindowController[0];
        private GameObject _openedWindowsContainer;

        
        public IWindowController Current { get; private set; }
        public bool IsBlockedInput { get; private set; }

        
        public WindowManager(ZenjectWindowFactory zenjectWindowFactory, IUpdateManager updateManager){
            _zenjectWindowFactory = zenjectWindowFactory;
            _updateManager = updateManager;
        }
        
        public void Initialize(){
            _updateManager.Register(this);
            _openedWindowsContainer = new GameObject("[OpenedWindowsContainer]");
            GameObject.DontDestroyOnLoad(_openedWindowsContainer);
        }
        
        public async Task<T> Open<T>(string path, bool autoInitialize = true) where T : class, IWindowController {
            var window = Get<T>();
            if (window != null) return window;

            window = _zenjectWindowFactory.CreateWindow<T>();
            await window.Initialize(_openedWindowsContainer.transform);
            window.OnOpen();
            
            return window;
        }
        
        public void Close<T>(T window) where T : class, IWindowController {
            if (window == null) return;
            
            window.OnClose();
            _openedWindows = _openedWindows.Where(x => x != window).ToArray();
            Current = _openedWindows.LastOrDefault();
        }
        
        public void CloseAll<T>() where T : class, IWindowController {
            var windows = _openedWindows.Where(x => x is T).ToArray();
            foreach (var window in windows) {
                Close(window as T);
            }
        }

        public T Get<T>() where T : class, IWindowController {
            return _openedWindows.FirstOrDefault(x => x is T) as T;
        }

        public void OnUpdate(float deltaTime) {
            foreach (var window in _openedWindows) {
                if(window is IUpdatable updatableWindow)
                    updatableWindow.OnUpdate(deltaTime);
            }
        }
        
        public void OnFixedUpdate(float deltaTime) {
            foreach (var window in _openedWindows) {
                if(window is IFixedUpdatable updatableWindow)
                    updatableWindow.OnFixedUpdate(deltaTime);
            }
        }
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