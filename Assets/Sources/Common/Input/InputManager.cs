using System;
using System.Linq;
using Sources.Project.Game.Constants;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using Zenject;

namespace Sources.Common.Input {
    public interface IInputService {
        bool GetButtonUp(string action);
        bool GetButton(string action);
        bool GetButtonDown(string action);
        InputDevice GetInputDevice();
        float GetAxis(string axis);
        (Vector2, bool) GetVector(string actionName);
        Vector2? GetVector(string actionName, InputSchemeType schemeType);
        bool IsPositive(string action);
    }

    [RequireComponent(typeof(EventSystem))]
    [RequireComponent(typeof(InputSystemUIInputModule))]
    public sealed class InputService : IInputService, IInitializable {
        public static InputControlsBinding Binding;
        public static InputDevice CurrentDevice;

        private Transform _infrastructureParent;

        private static event Action<InputSchemeType, InputDeviceType> _activeDeviceChanged;

        public static event Action<InputSchemeType, InputDeviceType> ActiveDeviceChanged {
            add {
                _activeDeviceChanged += value;
                var inputInfo = GetInputInfo(CurrentDevice);
                _activeDeviceChanged?.Invoke(inputInfo.Item1, inputInfo.Item2);
            }

            remove { _activeDeviceChanged -= value; }
        }

        public InputService(Transform parent){
            _infrastructureParent = parent;
        }

        ~ InputService() {
            Binding.FindAction(InputConstants.SystemAnyButton).performed -= InputSystem_OnAnyButtonPerformed;
            Binding.FindAction(InputConstants.SystemMouse).performed -= InputSystem_OnAnyButtonPerformed;
        }

        public void Initialize(){
            var inputSystemUIInputModule = new GameObject($"[{typeof(InputSystemUIInputModule).Name}]").AddComponent<InputSystemUIInputModule>();
            inputSystemUIInputModule.transform.parent = _infrastructureParent;

            Binding = new InputControlsBinding();
            Binding.Enable();
            Binding.FindAction(InputConstants.SystemAnyButton).performed += InputSystem_OnAnyButtonPerformed;
            Binding.FindAction(InputConstants.SystemMouse).performed += InputSystem_OnAnyButtonPerformed;
            
            UpdateInputDevice(InputSystem.devices.FirstOrDefault());
        }

        public bool GetButtonUp(string action) {
            var input = Binding.FindAction(action);
            return input != null && input.WasReleasedThisFrame();
        }

        public bool GetButton(string action) {
            var input = Binding.FindAction(action);
            return input != null && input.IsPressed();
        }

        public bool GetButtonDown(string action) {
            var input = Binding.FindAction(action);
            return input != null && input.WasPressedThisFrame();
        }

        public InputDevice GetInputDevice() {
            return CurrentDevice;
        }

        public float GetAxis(string axis) {
            var input = Binding.FindAction(axis);
            return input != null ? input.ReadValue<float>() : 0f;
        }

        public (Vector2, bool) GetVector(string actionName) {
            var input = Binding.FindAction(actionName);
            var vector = input != null ? input.ReadValue<Vector2>() : Vector2.zero;
            var isPressed = input.WasPerformedThisFrame() && input.WasPressedThisFrame();
            return (vector, isPressed);
        }
        
        public Vector2? GetVector(string actionName, InputSchemeType schemeType) {
            var action = Binding.FindAction(actionName);
            foreach (var control in action.controls) {
                var binding = action.GetBindingForControl(control);
                if (binding != null && binding.Value.groups == schemeType.ToString()) {
                    var value = control as InputControl<Vector2>;
                    if (value != null) {
                        return value.ReadValue();
                    }
                }
            }
            return null;
        }

        public bool IsPositive(string action) {
            return GetAxis(action) > 0f;
        }

        public static string GetKeyCodeByAction(string actionPath, InputSchemeType inputSchemeType) {
            if (string.IsNullOrEmpty(actionPath)) return string.Empty;
            
            var action = Binding?.FindAction(actionPath);
            if (action == null) return null;
            
            foreach (var binding in action.bindings) {
                var actionName = actionPath.Split("/").LastOrDefault();
                if (binding.action == actionName && binding.groups == inputSchemeType.ToString()) {
                    return InputControlPath.ToHumanReadableString(binding.effectivePath,
                        InputControlPath.HumanReadableStringOptions.OmitDevice);
                }
            }
            return string.Empty;
        }

        public static string GetKeyCodeByAction(string actionPath) {
            var inputInfo = GetInputInfo(CurrentDevice);
            return GetKeyCodeByAction(actionPath, inputInfo.Item1);
        }
        
        public static bool IsComposite(string actionPath, InputSchemeType inputSchemeType) {
            if (string.IsNullOrEmpty(actionPath)) return false;
            
            var action = Binding?.FindAction(actionPath);
            if (action == null) return false;

            foreach (var binding in action.bindings) {
                var actionName = actionPath.Split("/").LastOrDefault();
                if (binding.action == actionName && 
                    binding.isComposite && 
                    inputSchemeType == InputSchemeType.Keyboard) {
                    return true;
                }
            }

            return false;
        }
        
        public static string[] GetKeyCodesByAction(string actionPath, bool separateComposite = true) {
            var inputInfo = GetInputInfo(CurrentDevice);
            var isComposite = IsComposite(actionPath, inputInfo.Item1);
            if (separateComposite && isComposite) {
                return SeparateComposite(actionPath, inputInfo.Item1);
            }
            
            var keycode = GetKeyCodeByAction(actionPath, inputInfo.Item1);
            return new[] { keycode };
        }

        public static string[] SeparateComposite(string actionPath, InputSchemeType inputSchemeType) {
            var result = new string[0];
            if (string.IsNullOrEmpty(actionPath)) return result;
            
            var action = Binding?.FindAction(actionPath);
            if (action == null) return result;
            
            foreach (var binding in action.bindings) {
                var actionName = actionPath.Split("/").LastOrDefault();
                if (binding.action == actionName && binding.isPartOfComposite && binding.groups == inputSchemeType.ToString()) {
                    var keyCode = InputControlPath.ToHumanReadableString(binding.effectivePath,
                        InputControlPath.HumanReadableStringOptions.OmitDevice);
                    result = result.Append(keyCode).ToArray();
                }
            }
            return result;
        }
        
        public static InputSchemeType GetInputScheme(string actionName) {
            var inputInfo = GetInputInfo(CurrentDevice);
            return inputInfo.Item1;
        }

        private void InputSystem_OnAnyButtonPerformed(InputAction.CallbackContext context) {
            UpdateInputDevice(context.action.activeControl.device);
        }

        private static void UpdateInputDevice(InputDevice device) {
            if (CurrentDevice != device) {
                CurrentDevice = device;

                var inputInfo = GetInputInfo(CurrentDevice);
                _activeDeviceChanged?.Invoke(inputInfo.Item1, inputInfo.Item2);
            }
        }

        public static (InputSchemeType, InputDeviceType) GetInputInfo() {
            return GetInputInfo(CurrentDevice);
        }

        public static (InputSchemeType, InputDeviceType) GetInputInfo(InputDevice device) {
            var schemeType = InputSchemeType.None;
            var deviceType = InputDeviceType.None;

#if UNITY_STANDALONE || UNITY_EDITOR
            if (device is Keyboard || device is Mouse) {
                deviceType = InputDeviceType.Keyboard;
                schemeType = InputSchemeType.Keyboard;
            }
            else if (device is UnityEngine.InputSystem.DualShock.DualShockGamepad) {
                deviceType = InputDeviceType.DualSense;
                schemeType = InputSchemeType.Gamepad;
            }
            else if (device is UnityEngine.InputSystem.XInput.XInputController) {
                deviceType = InputDeviceType.Xbox;
                schemeType = InputSchemeType.Gamepad;
            }
            else if (device is UnityEngine.InputSystem.Switch.SwitchProControllerHID) {
                deviceType = InputDeviceType.Switch;
                schemeType = InputSchemeType.Gamepad;
            }
            else {
                deviceType = InputDeviceType.Xbox;
                schemeType = InputSchemeType.Gamepad;
            }
#elif UNITY_SWITCH
                deviceType = InputDeviceType.Xbox;
                schemeType = InputSchemeType.Gamepad;
#elif UNITY_GAMECORE
                deviceType = InputDeviceType.Xbox;
                schemeType = InputSchemeType.Gamepad;
#elif UNITY_PLAYSTATION
                deviceType = InputDeviceType.DualSense;
                schemeType = InputSchemeType.Gamepad;
#endif
            return (schemeType, deviceType);
        }
    }
}