// GENERATED AUTOMATICALLY FROM 'Assets/Input/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class PlayerControls : IInputActionCollection, IDisposable
{
    private InputActionAsset asset;
    public PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""400c2d2f-14e8-4b39-88ad-2a860b688e3e"",
            ""actions"": [
                {
                    ""name"": ""MeleeAttack"",
                    ""type"": ""Button"",
                    ""id"": ""82754275-4c2a-408b-b69d-0f6aacb8b231"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Rotate"",
                    ""type"": ""Value"",
                    ""id"": ""0d53b353-abe1-496f-baaa-f76bafde2258"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interactive"",
                    ""type"": ""Button"",
                    ""id"": ""62e79bde-68e0-4595-a083-abc81b29ad81"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Spell1"",
                    ""type"": ""Button"",
                    ""id"": ""bfdf56b8-024e-4023-bab3-963540548eb3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Spell2"",
                    ""type"": ""Button"",
                    ""id"": ""0cf8afd1-6deb-4781-8ccb-0fa62bc8dc74"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Walk"",
                    ""type"": ""Button"",
                    ""id"": ""d2796715-57b6-41c8-afd2-a0528bf31a1b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""b10267cb-8a6e-4fca-8984-9a38a60a6700"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AngleChange"",
                    ""type"": ""Button"",
                    ""id"": ""e4d2fc1a-fa4b-4232-aeaa-5950d586bb6c"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a870dab3-1a06-4b4d-8a57-4b2e5bf817d1"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All Control Schemes;Gamepad"",
                    ""action"": ""MeleeAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ff09fc07-2304-448d-9883-4895419600a9"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All Control Schemes;Gamepad"",
                    ""action"": ""MeleeAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""19d6bd27-d00c-4e25-8598-dc6aa9677e87"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All Control Schemes;PC"",
                    ""action"": ""MeleeAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""149f4c5c-0288-4b31-a002-ba4825caf62f"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All Control Schemes;Gamepad"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5a6cba0a-622a-4dfc-a422-d63c2d8d9a3c"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All Control Schemes;Gamepad"",
                    ""action"": ""Interactive"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""707c10e6-94a3-4ac8-b3d2-98413ed02cd5"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All Control Schemes;PC"",
                    ""action"": ""Interactive"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""049d76c0-1a6a-4eb5-afc2-2ca5d4f12d9c"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All Control Schemes;Gamepad"",
                    ""action"": ""Spell1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""282295f3-72f3-4dea-bbbc-73470de0c010"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All Control Schemes;PC"",
                    ""action"": ""Spell1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ff690d5a-42f6-452e-a302-4934377273d6"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC;All Control Schemes"",
                    ""action"": ""Spell1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c75d8b63-3411-406f-a537-8e9de1725a6b"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All Control Schemes;Gamepad"",
                    ""action"": ""Spell2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""70c9dd8b-13d3-4017-93f3-42eba2694545"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All Control Schemes;PC"",
                    ""action"": ""Spell2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a07ad2d7-9e21-4a7c-837f-3ffb8cd101e7"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All Control Schemes;PC"",
                    ""action"": ""Spell2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6c45deba-4a27-4e6b-b1fc-27399a040782"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Walk"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f78e3ee8-4392-45fc-999f-661cee01d8c1"",
                    ""path"": ""<Keyboard>/rightShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Walk"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d2c51224-251f-495f-909a-b39b5e778c25"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All Control Schemes;Gamepad"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b98c336c-37c9-4ea3-8484-965452395127"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All Control Schemes;PC"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6129b89f-7816-478e-8b95-9afd42094cd4"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All Control Schemes;Gamepad"",
                    ""action"": ""AngleChange"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""348786b4-bc7b-4df2-9bfb-27a0d1933e39"",
                    ""path"": ""<Keyboard>/t"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All Control Schemes;PC"",
                    ""action"": ""AngleChange"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""All Control Schemes"",
            ""bindingGroup"": ""All Control Schemes"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""PC"",
            ""bindingGroup"": ""PC"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_MeleeAttack = m_Gameplay.FindAction("MeleeAttack", throwIfNotFound: true);
        m_Gameplay_Rotate = m_Gameplay.FindAction("Rotate", throwIfNotFound: true);
        m_Gameplay_Interactive = m_Gameplay.FindAction("Interactive", throwIfNotFound: true);
        m_Gameplay_Spell1 = m_Gameplay.FindAction("Spell1", throwIfNotFound: true);
        m_Gameplay_Spell2 = m_Gameplay.FindAction("Spell2", throwIfNotFound: true);
        m_Gameplay_Walk = m_Gameplay.FindAction("Walk", throwIfNotFound: true);
        m_Gameplay_Pause = m_Gameplay.FindAction("Pause", throwIfNotFound: true);
        m_Gameplay_AngleChange = m_Gameplay.FindAction("AngleChange", throwIfNotFound: true);
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

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_MeleeAttack;
    private readonly InputAction m_Gameplay_Rotate;
    private readonly InputAction m_Gameplay_Interactive;
    private readonly InputAction m_Gameplay_Spell1;
    private readonly InputAction m_Gameplay_Spell2;
    private readonly InputAction m_Gameplay_Walk;
    private readonly InputAction m_Gameplay_Pause;
    private readonly InputAction m_Gameplay_AngleChange;
    public struct GameplayActions
    {
        private PlayerControls m_Wrapper;
        public GameplayActions(PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @MeleeAttack => m_Wrapper.m_Gameplay_MeleeAttack;
        public InputAction @Rotate => m_Wrapper.m_Gameplay_Rotate;
        public InputAction @Interactive => m_Wrapper.m_Gameplay_Interactive;
        public InputAction @Spell1 => m_Wrapper.m_Gameplay_Spell1;
        public InputAction @Spell2 => m_Wrapper.m_Gameplay_Spell2;
        public InputAction @Walk => m_Wrapper.m_Gameplay_Walk;
        public InputAction @Pause => m_Wrapper.m_Gameplay_Pause;
        public InputAction @AngleChange => m_Wrapper.m_Gameplay_AngleChange;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                MeleeAttack.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMeleeAttack;
                MeleeAttack.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMeleeAttack;
                MeleeAttack.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMeleeAttack;
                Rotate.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRotate;
                Rotate.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRotate;
                Rotate.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRotate;
                Interactive.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnInteractive;
                Interactive.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnInteractive;
                Interactive.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnInteractive;
                Spell1.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSpell1;
                Spell1.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSpell1;
                Spell1.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSpell1;
                Spell2.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSpell2;
                Spell2.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSpell2;
                Spell2.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSpell2;
                Walk.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnWalk;
                Walk.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnWalk;
                Walk.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnWalk;
                Pause.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPause;
                Pause.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPause;
                Pause.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPause;
                AngleChange.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAngleChange;
                AngleChange.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAngleChange;
                AngleChange.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAngleChange;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                MeleeAttack.started += instance.OnMeleeAttack;
                MeleeAttack.performed += instance.OnMeleeAttack;
                MeleeAttack.canceled += instance.OnMeleeAttack;
                Rotate.started += instance.OnRotate;
                Rotate.performed += instance.OnRotate;
                Rotate.canceled += instance.OnRotate;
                Interactive.started += instance.OnInteractive;
                Interactive.performed += instance.OnInteractive;
                Interactive.canceled += instance.OnInteractive;
                Spell1.started += instance.OnSpell1;
                Spell1.performed += instance.OnSpell1;
                Spell1.canceled += instance.OnSpell1;
                Spell2.started += instance.OnSpell2;
                Spell2.performed += instance.OnSpell2;
                Spell2.canceled += instance.OnSpell2;
                Walk.started += instance.OnWalk;
                Walk.performed += instance.OnWalk;
                Walk.canceled += instance.OnWalk;
                Pause.started += instance.OnPause;
                Pause.performed += instance.OnPause;
                Pause.canceled += instance.OnPause;
                AngleChange.started += instance.OnAngleChange;
                AngleChange.performed += instance.OnAngleChange;
                AngleChange.canceled += instance.OnAngleChange;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    private int m_AllControlSchemesSchemeIndex = -1;
    public InputControlScheme AllControlSchemesScheme
    {
        get
        {
            if (m_AllControlSchemesSchemeIndex == -1) m_AllControlSchemesSchemeIndex = asset.FindControlSchemeIndex("All Control Schemes");
            return asset.controlSchemes[m_AllControlSchemesSchemeIndex];
        }
    }
    private int m_PCSchemeIndex = -1;
    public InputControlScheme PCScheme
    {
        get
        {
            if (m_PCSchemeIndex == -1) m_PCSchemeIndex = asset.FindControlSchemeIndex("PC");
            return asset.controlSchemes[m_PCSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IGameplayActions
    {
        void OnMeleeAttack(InputAction.CallbackContext context);
        void OnRotate(InputAction.CallbackContext context);
        void OnInteractive(InputAction.CallbackContext context);
        void OnSpell1(InputAction.CallbackContext context);
        void OnSpell2(InputAction.CallbackContext context);
        void OnWalk(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnAngleChange(InputAction.CallbackContext context);
    }
}
