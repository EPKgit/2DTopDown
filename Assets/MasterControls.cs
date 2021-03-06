// GENERATED AUTOMATICALLY FROM 'Assets/MasterControls.inputactions'

using System;
using UnityEngine;
using UnityEngine.Experimental.Input;


[Serializable]
public class MasterControls : InputActionAssetReference
{
    public MasterControls()
    {
    }
    public MasterControls(InputActionAsset asset)
        : base(asset)
    {
    }
    private bool m_Initialized;
    private void Initialize()
    {
        // Gameplay
        m_Gameplay = asset.GetActionMap("Gameplay");
        m_Gameplay_Movement = m_Gameplay.GetAction("Movement");
        m_Gameplay_Attack = m_Gameplay.GetAction("Attack");
        m_Gameplay_Ability1 = m_Gameplay.GetAction("Ability1");
        m_Gameplay_Ability2 = m_Gameplay.GetAction("Ability2");
        m_Gameplay_Ability3 = m_Gameplay.GetAction("Ability3");
        m_Gameplay_Interact = m_Gameplay.GetAction("Interact");
        m_Gameplay_AimDirection = m_Gameplay.GetAction("AimDirection");
        // Menu
        m_Menu = asset.GetActionMap("Menu");
        m_Menu_Join = m_Menu.GetAction("Join");
        m_Menu_Back = m_Menu.GetAction("Back");
        m_Menu_Continue = m_Menu.GetAction("Continue");
        m_Menu_Select = m_Menu.GetAction("Select");
        m_Initialized = true;
    }
    private void Uninitialize()
    {
        if (m_GameplayActionsCallbackInterface != null)
        {
            Gameplay.SetCallbacks(null);
        }
        m_Gameplay = null;
        m_Gameplay_Movement = null;
        m_Gameplay_Attack = null;
        m_Gameplay_Ability1 = null;
        m_Gameplay_Ability2 = null;
        m_Gameplay_Ability3 = null;
        m_Gameplay_Interact = null;
        m_Gameplay_AimDirection = null;
        if (m_MenuActionsCallbackInterface != null)
        {
            Menu.SetCallbacks(null);
        }
        m_Menu = null;
        m_Menu_Join = null;
        m_Menu_Back = null;
        m_Menu_Continue = null;
        m_Menu_Select = null;
        m_Initialized = false;
    }
    public void SetAsset(InputActionAsset newAsset)
    {
        if (newAsset == asset) return;
        var GameplayCallbacks = m_GameplayActionsCallbackInterface;
        var MenuCallbacks = m_MenuActionsCallbackInterface;
        if (m_Initialized) Uninitialize();
        asset = newAsset;
        Gameplay.SetCallbacks(GameplayCallbacks);
        Menu.SetCallbacks(MenuCallbacks);
    }
    public override void MakePrivateCopyOfActions()
    {
        SetAsset(ScriptableObject.Instantiate(asset));
    }
    // Gameplay
    private InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private InputAction m_Gameplay_Movement;
    private InputAction m_Gameplay_Attack;
    private InputAction m_Gameplay_Ability1;
    private InputAction m_Gameplay_Ability2;
    private InputAction m_Gameplay_Ability3;
    private InputAction m_Gameplay_Interact;
    private InputAction m_Gameplay_AimDirection;
    public struct GameplayActions
    {
        private MasterControls m_Wrapper;
        public GameplayActions(MasterControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement { get { return m_Wrapper.m_Gameplay_Movement; } }
        public InputAction @Attack { get { return m_Wrapper.m_Gameplay_Attack; } }
        public InputAction @Ability1 { get { return m_Wrapper.m_Gameplay_Ability1; } }
        public InputAction @Ability2 { get { return m_Wrapper.m_Gameplay_Ability2; } }
        public InputAction @Ability3 { get { return m_Wrapper.m_Gameplay_Ability3; } }
        public InputAction @Interact { get { return m_Wrapper.m_Gameplay_Interact; } }
        public InputAction @AimDirection { get { return m_Wrapper.m_Gameplay_AimDirection; } }
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled { get { return Get().enabled; } }
        public InputActionMap Clone() { return Get().Clone(); }
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                Movement.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
                Movement.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
                Movement.cancelled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
                Attack.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAttack;
                Attack.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAttack;
                Attack.cancelled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAttack;
                Ability1.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAbility1;
                Ability1.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAbility1;
                Ability1.cancelled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAbility1;
                Ability2.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAbility2;
                Ability2.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAbility2;
                Ability2.cancelled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAbility2;
                Ability3.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAbility3;
                Ability3.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAbility3;
                Ability3.cancelled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAbility3;
                Interact.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnInteract;
                Interact.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnInteract;
                Interact.cancelled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnInteract;
                AimDirection.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAimDirection;
                AimDirection.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAimDirection;
                AimDirection.cancelled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAimDirection;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                Movement.started += instance.OnMovement;
                Movement.performed += instance.OnMovement;
                Movement.cancelled += instance.OnMovement;
                Attack.started += instance.OnAttack;
                Attack.performed += instance.OnAttack;
                Attack.cancelled += instance.OnAttack;
                Ability1.started += instance.OnAbility1;
                Ability1.performed += instance.OnAbility1;
                Ability1.cancelled += instance.OnAbility1;
                Ability2.started += instance.OnAbility2;
                Ability2.performed += instance.OnAbility2;
                Ability2.cancelled += instance.OnAbility2;
                Ability3.started += instance.OnAbility3;
                Ability3.performed += instance.OnAbility3;
                Ability3.cancelled += instance.OnAbility3;
                Interact.started += instance.OnInteract;
                Interact.performed += instance.OnInteract;
                Interact.cancelled += instance.OnInteract;
                AimDirection.started += instance.OnAimDirection;
                AimDirection.performed += instance.OnAimDirection;
                AimDirection.cancelled += instance.OnAimDirection;
            }
        }
    }
    public GameplayActions @Gameplay
    {
        get
        {
            if (!m_Initialized) Initialize();
            return new GameplayActions(this);
        }
    }
    // Menu
    private InputActionMap m_Menu;
    private IMenuActions m_MenuActionsCallbackInterface;
    private InputAction m_Menu_Join;
    private InputAction m_Menu_Back;
    private InputAction m_Menu_Continue;
    private InputAction m_Menu_Select;
    public struct MenuActions
    {
        private MasterControls m_Wrapper;
        public MenuActions(MasterControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Join { get { return m_Wrapper.m_Menu_Join; } }
        public InputAction @Back { get { return m_Wrapper.m_Menu_Back; } }
        public InputAction @Continue { get { return m_Wrapper.m_Menu_Continue; } }
        public InputAction @Select { get { return m_Wrapper.m_Menu_Select; } }
        public InputActionMap Get() { return m_Wrapper.m_Menu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled { get { return Get().enabled; } }
        public InputActionMap Clone() { return Get().Clone(); }
        public static implicit operator InputActionMap(MenuActions set) { return set.Get(); }
        public void SetCallbacks(IMenuActions instance)
        {
            if (m_Wrapper.m_MenuActionsCallbackInterface != null)
            {
                Join.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnJoin;
                Join.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnJoin;
                Join.cancelled -= m_Wrapper.m_MenuActionsCallbackInterface.OnJoin;
                Back.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnBack;
                Back.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnBack;
                Back.cancelled -= m_Wrapper.m_MenuActionsCallbackInterface.OnBack;
                Continue.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnContinue;
                Continue.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnContinue;
                Continue.cancelled -= m_Wrapper.m_MenuActionsCallbackInterface.OnContinue;
                Select.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnSelect;
                Select.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnSelect;
                Select.cancelled -= m_Wrapper.m_MenuActionsCallbackInterface.OnSelect;
            }
            m_Wrapper.m_MenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                Join.started += instance.OnJoin;
                Join.performed += instance.OnJoin;
                Join.cancelled += instance.OnJoin;
                Back.started += instance.OnBack;
                Back.performed += instance.OnBack;
                Back.cancelled += instance.OnBack;
                Continue.started += instance.OnContinue;
                Continue.performed += instance.OnContinue;
                Continue.cancelled += instance.OnContinue;
                Select.started += instance.OnSelect;
                Select.performed += instance.OnSelect;
                Select.cancelled += instance.OnSelect;
            }
        }
    }
    public MenuActions @Menu
    {
        get
        {
            if (!m_Initialized) Initialize();
            return new MenuActions(this);
        }
    }
}
public interface IGameplayActions
{
    void OnMovement(InputAction.CallbackContext context);
    void OnAttack(InputAction.CallbackContext context);
    void OnAbility1(InputAction.CallbackContext context);
    void OnAbility2(InputAction.CallbackContext context);
    void OnAbility3(InputAction.CallbackContext context);
    void OnInteract(InputAction.CallbackContext context);
    void OnAimDirection(InputAction.CallbackContext context);
}
public interface IMenuActions
{
    void OnJoin(InputAction.CallbackContext context);
    void OnBack(InputAction.CallbackContext context);
    void OnContinue(InputAction.CallbackContext context);
    void OnSelect(InputAction.CallbackContext context);
}
