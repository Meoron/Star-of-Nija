using Sources.Common.Input;
using TMPro;
using UnityEngine;

namespace Sources.Unity.Platform.Localization {
    [RequireComponent(typeof(TextMeshProUGUI))]
    public sealed class TextMeshLocalizationComponent : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI _textMeshPro;
        [SerializeField] private string _localKey;
        [SerializeField] private string _action;
        
#if UNITY_EDITOR
        private void Reset() {
            if (Application.isPlaying) return;
            if (_textMeshPro == null) {
                _textMeshPro = GetComponent<TextMeshProUGUI>();
            }

            if (string.IsNullOrEmpty(_localKey)) {
                _localKey = "localization_key";
            }
        }
#endif

        private void OnEnable() {
            /*LocalizationService.Instance.OnChangeLocalization += LocalizationManager_OnLocalizationChanged;
            InputManager.ActiveDeviceChanged += InputManager_OnActiveDeviceChanged;*/
        }

        private void OnDisable() {
            /*InputManager.ActiveDeviceChanged -= InputManager_OnActiveDeviceChanged;
            LocalizationService.Instance.OnChangeLocalization -= LocalizationManager_OnLocalizationChanged;*/
        }

        private void LocalizationManager_OnLocalizationChanged() {
            /*var keyCode = InputManager.GetKeyCodeByAction(_action);
            _textMeshPro.text = $"{_localKey}".GetTranslate(InputManager.CurrentDevice, keyCode);*/
        }
        
        private void InputManager_OnActiveDeviceChanged(InputSchemeType scheme, InputDeviceType device) {
            /*var keyName = InputManager.GetKeyCodeByAction(_action, scheme);
            _textMeshPro.text = $"{_localKey}".GetTranslate(device, keyName);*/ 
        }
    }
}