// GENERATED AUTOMATICALLY FROM 'Assets/Input/PlayerControls.inputactions'

using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class PlayerControls : IInputActionCollection
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
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""8da818ae-023b-46bc-a8ea-2639f8ef2125"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Rotate"",
                    ""type"": ""Button"",
                    ""id"": ""0d53b353-abe1-496f-baaa-f76bafde2258"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interactive"",
                    ""type"": ""Button"",
                    ""id"": ""62e79bde-68e0-4595-a083-abc81b29ad81"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Spell1"",
                    ""type"": ""Button"",
                    ""id"": ""bfdf56b8-024e-4023-bab3-963540548eb3"",
                    ""expectedControlType"": """",
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
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a870dab3-1a06-4b4d-8a57-4b2e5bf817d1"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
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
                    ""groups"": """",
                    ""action"": ""MeleeAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a5e0367e-d337-4c29-8580-cf30d51e5813"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""149f4c5c-0288-4b31-a002-ba4825caf62f"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
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
                    ""groups"": """",
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
                    ""groups"": """",
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
                    ""groups"": """",
                    ""action"": ""Spell2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_MeleeAttack = m_Gameplay.FindAction("MeleeAttack", throwIfNotFound: true);
        m_Gameplay_Move = m_Gameplay.FindAction("Move", throwIfNotFound: true);
        m_Gameplay_Rotate = m_Gameplay.FindAction("Rotate", throwIfNotFound: true);
        m_Gameplay_Interactive = m_Gameplay.FindAction("Interactive", throwIfNotFound: true);
        m_Gameplay_Spell1 = m_Gameplay.FindAction("Spell1", throwIfNotFound: true);
        m_Gameplay_Spell2 = m_Gameplay.FindAction("Spell2", throwIfNotFound: true);
    }

    ~PlayerControls()
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
    private readonly InputAction m_Gameplay_Move;
    private readonly InputAction m_Gameplay_Rotate;
    private readonly InputAction m_Gameplay_Interactive;
    private readonly InputAction m_Gameplay_Spell1;
    private readonly InputAction m_Gameplay_Spell2;
    public struct GameplayActions
    {
        private PlayerControls m_Wrapper;
        public GameplayActions(PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @MeleeAttack => m_Wrapper.m_Gameplay_MeleeAttack;
        public InputAction @Move => m_Wrapper.m_Gameplay_Move;
        public InputAction @Rotate => m_Wrapper.m_Gameplay_Rotate;
        public InputAction @Interactive => m_Wrapper.m_Gameplay_Interactive;
        public InputAction @Spell1 => m_Wrapper.m_Gameplay_Spell1;
        public InputAction @Spell2 => m_Wrapper.m_Gameplay_Spell2;
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
                Move.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                Move.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                Move.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
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
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                MeleeAttack.started += instance.OnMeleeAttack;
                MeleeAttack.performed += instance.OnMeleeAttack;
                MeleeAttack.canceled += instance.OnMeleeAttack;
                Move.started += instance.OnMove;
                Move.performed += instance.OnMove;
                Move.canceled += instance.OnMove;
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
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    public interface IGameplayActions
    {
        void OnMeleeAttack(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnRotate(InputAction.CallbackContext context);
        void OnInteractive(InputAction.CallbackContext context);
        void OnSpell1(InputAction.CallbackContext context);
        void OnSpell2(InputAction.CallbackContext context);
    }
}
