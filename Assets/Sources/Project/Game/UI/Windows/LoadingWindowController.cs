using DG.Tweening;
using Sources.Project.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Project.UI.Windows {
    public sealed class LoadingWindowController : WindowController {
        [SerializeField] private Image _imageFader;
        [SerializeField] private GameObject _loadingIcon;

        public bool IsInProgress;
        
        public override LayoutLevel GetLayout() {
            return LayoutLevel.Overlay;
        }

        protected override void OnInitialize() {
            IsInProgress = true;
            if(_loadingIcon!=null) _loadingIcon.SetActive(false);
            _imageFader.color = Color.clear;
            _imageFader.DOColor(Color.black, 1f/*GameConfig.LoadingFadeInOutTime*/).OnComplete(() => {
                if(_loadingIcon!=null) _loadingIcon?.SetActive(true);
                IsInProgress = false;
            });
        }

        public override void Close() {
            if(_loadingIcon!=null) _loadingIcon.SetActive(false);
            _imageFader.DOColor(Vector4.zero, 1f/*GameConfig.LoadingFadeInOutTime*/).OnComplete(() => {
                base.Close();
            });
        }

        protected override void OnRelease() {
        }

        public override void OnDrawUpdate() {
        }
        
        public override void OnUpdate() {
        }

        public override void OnFixedUpdate() {
        }
    }
}