// GENERATED AUTOMATICALLY FROM 'Assets/Controls/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""InGameBard"",
            ""id"": ""49697476-3718-449b-91f4-8b73e806316c"",
            ""actions"": [
                {
                    ""name"": ""PressKey"",
                    ""type"": ""Button"",
                    ""id"": ""834eca2f-6012-4f3e-b74e-85b9c37ed2cb"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""fca4809f-fa74-45d0-a685-966e6d2c8cb8"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8847a0f6-2e8b-4b71-9c48-ccf2ea0ba1c5"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PressKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""aa773f14-be36-4a58-ab2d-5e248ed37c90"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PressKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4a1e6204-f3b9-403a-aab1-b68b25032ef2"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PressKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ad03b6da-fdab-4f10-8988-ae81420d0da5"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PressKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7aa816e0-795e-489a-93fc-3af338fdfd1e"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PressKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ecee826d-a518-4be6-9bd0-017cb76d95f5"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PressKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""610de5ef-9d53-4213-9f73-c4201044a54c"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PressKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5b9a75be-d95e-4a82-af2f-150e775cab35"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PressKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6168e299-e45d-4129-8be2-f38a8d2f62cb"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""41fe6ae1-d149-441b-a6a2-f251008e8b3d"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""InMenu"",
            ""id"": ""0fc6c08e-2367-4350-b398-1c818194ba03"",
            ""actions"": [
                {
                    ""name"": ""AnyKey"",
                    ""type"": ""Button"",
                    ""id"": ""693d4407-2af8-4bbe-a01b-c5eb3afa25fa"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e96bb143-5e31-448e-b42a-213eab04f3b0"",
                    ""path"": ""<Keyboard>/anyKey"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AnyKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // InGameBard
        m_InGameBard = asset.FindActionMap("InGameBard", throwIfNotFound: true);
        m_InGameBard_PressKey = m_InGameBard.FindAction("PressKey", throwIfNotFound: true);
        m_InGameBard_Pause = m_InGameBard.FindAction("Pause", throwIfNotFound: true);
        // InMenu
        m_InMenu = asset.FindActionMap("InMenu", throwIfNotFound: true);
        m_InMenu_AnyKey = m_InMenu.FindAction("AnyKey", throwIfNotFound: true);
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

    // InGameBard
    private readonly InputActionMap m_InGameBard;
    private IInGameBardActions m_InGameBardActionsCallbackInterface;
    private readonly InputAction m_InGameBard_PressKey;
    private readonly InputAction m_InGameBard_Pause;
    public struct InGameBardActions
    {
        private @Controls m_Wrapper;
        public InGameBardActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @PressKey => m_Wrapper.m_InGameBard_PressKey;
        public InputAction @Pause => m_Wrapper.m_InGameBard_Pause;
        public InputActionMap Get() { return m_Wrapper.m_InGameBard; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InGameBardActions set) { return set.Get(); }
        public void SetCallbacks(IInGameBardActions instance)
        {
            if (m_Wrapper.m_InGameBardActionsCallbackInterface != null)
            {
                @PressKey.started -= m_Wrapper.m_InGameBardActionsCallbackInterface.OnPressKey;
                @PressKey.performed -= m_Wrapper.m_InGameBardActionsCallbackInterface.OnPressKey;
                @PressKey.canceled -= m_Wrapper.m_InGameBardActionsCallbackInterface.OnPressKey;
                @Pause.started -= m_Wrapper.m_InGameBardActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_InGameBardActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_InGameBardActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_InGameBardActionsCallbackInterface = instance;
            if (instance != null)
            {
                @PressKey.started += instance.OnPressKey;
                @PressKey.performed += instance.OnPressKey;
                @PressKey.canceled += instance.OnPressKey;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }
        }
    }
    public InGameBardActions @InGameBard => new InGameBardActions(this);

    // InMenu
    private readonly InputActionMap m_InMenu;
    private IInMenuActions m_InMenuActionsCallbackInterface;
    private readonly InputAction m_InMenu_AnyKey;
    public struct InMenuActions
    {
        private @Controls m_Wrapper;
        public InMenuActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @AnyKey => m_Wrapper.m_InMenu_AnyKey;
        public InputActionMap Get() { return m_Wrapper.m_InMenu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InMenuActions set) { return set.Get(); }
        public void SetCallbacks(IInMenuActions instance)
        {
            if (m_Wrapper.m_InMenuActionsCallbackInterface != null)
            {
                @AnyKey.started -= m_Wrapper.m_InMenuActionsCallbackInterface.OnAnyKey;
                @AnyKey.performed -= m_Wrapper.m_InMenuActionsCallbackInterface.OnAnyKey;
                @AnyKey.canceled -= m_Wrapper.m_InMenuActionsCallbackInterface.OnAnyKey;
            }
            m_Wrapper.m_InMenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @AnyKey.started += instance.OnAnyKey;
                @AnyKey.performed += instance.OnAnyKey;
                @AnyKey.canceled += instance.OnAnyKey;
            }
        }
    }
    public InMenuActions @InMenu => new InMenuActions(this);
    public interface IInGameBardActions
    {
        void OnPressKey(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
    }
    public interface IInMenuActions
    {
        void OnAnyKey(InputAction.CallbackContext context);
    }
}
