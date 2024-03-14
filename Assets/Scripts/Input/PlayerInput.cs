//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Scripts/Input/PlayerInput.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInput: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""MouseObjects"",
            ""id"": ""f84e1229-141a-4e72-aaec-46b71e0db778"",
            ""actions"": [
                {
                    ""name"": ""MouseLeft"",
                    ""type"": ""Value"",
                    ""id"": ""1881faf0-050c-4f5a-9f7e-9cd587a94057"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MouseRight"",
                    ""type"": ""Button"",
                    ""id"": ""ce061f99-1b9a-449a-860c-0b38156ff893"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ExtendSelection"",
                    ""type"": ""Value"",
                    ""id"": ""58337831-014b-4b41-961f-b84ed3d1427f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MouseVector"",
                    ""type"": ""Value"",
                    ""id"": ""68d0e156-e26f-4162-821c-6955b893f133"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""4d58b146-83f5-46c3-b319-78f0610847a0"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8b448da4-06e9-4b82-a6e4-c149c164a7fa"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f2c1a0cb-5a8c-4bcb-9bf9-4d2a96a4c3d5"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ExtendSelection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8082f492-5b5c-4304-bd66-ce4728f588aa"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseVector"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // MouseObjects
        m_MouseObjects = asset.FindActionMap("MouseObjects", throwIfNotFound: true);
        m_MouseObjects_MouseLeft = m_MouseObjects.FindAction("MouseLeft", throwIfNotFound: true);
        m_MouseObjects_MouseRight = m_MouseObjects.FindAction("MouseRight", throwIfNotFound: true);
        m_MouseObjects_ExtendSelection = m_MouseObjects.FindAction("ExtendSelection", throwIfNotFound: true);
        m_MouseObjects_MouseVector = m_MouseObjects.FindAction("MouseVector", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // MouseObjects
    private readonly InputActionMap m_MouseObjects;
    private List<IMouseObjectsActions> m_MouseObjectsActionsCallbackInterfaces = new List<IMouseObjectsActions>();
    private readonly InputAction m_MouseObjects_MouseLeft;
    private readonly InputAction m_MouseObjects_MouseRight;
    private readonly InputAction m_MouseObjects_ExtendSelection;
    private readonly InputAction m_MouseObjects_MouseVector;
    public struct MouseObjectsActions
    {
        private @PlayerInput m_Wrapper;
        public MouseObjectsActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @MouseLeft => m_Wrapper.m_MouseObjects_MouseLeft;
        public InputAction @MouseRight => m_Wrapper.m_MouseObjects_MouseRight;
        public InputAction @ExtendSelection => m_Wrapper.m_MouseObjects_ExtendSelection;
        public InputAction @MouseVector => m_Wrapper.m_MouseObjects_MouseVector;
        public InputActionMap Get() { return m_Wrapper.m_MouseObjects; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MouseObjectsActions set) { return set.Get(); }
        public void AddCallbacks(IMouseObjectsActions instance)
        {
            if (instance == null || m_Wrapper.m_MouseObjectsActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_MouseObjectsActionsCallbackInterfaces.Add(instance);
            @MouseLeft.started += instance.OnMouseLeft;
            @MouseLeft.performed += instance.OnMouseLeft;
            @MouseLeft.canceled += instance.OnMouseLeft;
            @MouseRight.started += instance.OnMouseRight;
            @MouseRight.performed += instance.OnMouseRight;
            @MouseRight.canceled += instance.OnMouseRight;
            @ExtendSelection.started += instance.OnExtendSelection;
            @ExtendSelection.performed += instance.OnExtendSelection;
            @ExtendSelection.canceled += instance.OnExtendSelection;
            @MouseVector.started += instance.OnMouseVector;
            @MouseVector.performed += instance.OnMouseVector;
            @MouseVector.canceled += instance.OnMouseVector;
        }

        private void UnregisterCallbacks(IMouseObjectsActions instance)
        {
            @MouseLeft.started -= instance.OnMouseLeft;
            @MouseLeft.performed -= instance.OnMouseLeft;
            @MouseLeft.canceled -= instance.OnMouseLeft;
            @MouseRight.started -= instance.OnMouseRight;
            @MouseRight.performed -= instance.OnMouseRight;
            @MouseRight.canceled -= instance.OnMouseRight;
            @ExtendSelection.started -= instance.OnExtendSelection;
            @ExtendSelection.performed -= instance.OnExtendSelection;
            @ExtendSelection.canceled -= instance.OnExtendSelection;
            @MouseVector.started -= instance.OnMouseVector;
            @MouseVector.performed -= instance.OnMouseVector;
            @MouseVector.canceled -= instance.OnMouseVector;
        }

        public void RemoveCallbacks(IMouseObjectsActions instance)
        {
            if (m_Wrapper.m_MouseObjectsActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IMouseObjectsActions instance)
        {
            foreach (var item in m_Wrapper.m_MouseObjectsActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_MouseObjectsActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public MouseObjectsActions @MouseObjects => new MouseObjectsActions(this);
    public interface IMouseObjectsActions
    {
        void OnMouseLeft(InputAction.CallbackContext context);
        void OnMouseRight(InputAction.CallbackContext context);
        void OnExtendSelection(InputAction.CallbackContext context);
        void OnMouseVector(InputAction.CallbackContext context);
    }
}
