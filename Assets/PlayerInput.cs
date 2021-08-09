// GENERATED AUTOMATICALLY FROM 'Assets/PlayerInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""Freeroam"",
            ""id"": ""bc61846f-241d-4519-ad77-23607a7d8771"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""229f40da-2ba0-411f-9178-f2babaa96465"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""a7924e69-31ef-4696-a357-278ff5991869"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""98b012a2-9eca-4e8f-b409-7d1bc1f216e1"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""bde5d8a9-3d5e-444e-ad28-24db530f66da"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""bd0d43e9-f018-46c4-8a6b-33256a0c9319"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""97fa00fb-76c3-44db-9d69-c0755fa5bc0b"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Freeroam
        m_Freeroam = asset.FindActionMap("Freeroam", throwIfNotFound: true);
        m_Freeroam_Move = m_Freeroam.FindAction("Move", throwIfNotFound: true);
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

    // Freeroam
    private readonly InputActionMap m_Freeroam;
    private IFreeroamActions m_FreeroamActionsCallbackInterface;
    private readonly InputAction m_Freeroam_Move;
    public struct FreeroamActions
    {
        private @PlayerInput m_Wrapper;
        public FreeroamActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Freeroam_Move;
        public InputActionMap Get() { return m_Wrapper.m_Freeroam; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(FreeroamActions set) { return set.Get(); }
        public void SetCallbacks(IFreeroamActions instance)
        {
            if (m_Wrapper.m_FreeroamActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_FreeroamActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_FreeroamActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_FreeroamActionsCallbackInterface.OnMove;
            }
            m_Wrapper.m_FreeroamActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
            }
        }
    }
    public FreeroamActions @Freeroam => new FreeroamActions(this);
    public interface IFreeroamActions
    {
        void OnMove(InputAction.CallbackContext context);
    }
}
