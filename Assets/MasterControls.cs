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
        // Menu
        m_Menu = asset.GetActionMap("Menu");
        m_Menu_Join = m_Menu.GetAction("Join");
        m_Menu_Leave = m_Menu.GetAction("Leave");
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
        if (m_MenuActionsCallbackInterface != null)
        {
            Menu.SetCallbacks(null);
        }
        m_Menu = null;
        m_Menu_Join = null;
        m_Menu_Leave = null;
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
    public struct GameplayActions
    {
        private MasterControls m_Wrapper;
        public GameplayActions(MasterControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement { get { return m_Wrapper.m_Gameplay_Movement; } }
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
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                Movement.started += instance.OnMovement;
                Movement.performed += instance.OnMovement;
                Movement.cancelled += instance.OnMovement;
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
    private InputAction m_Menu_Leave;
    public struct MenuActions
    {
        private MasterControls m_Wrapper;
        public MenuActions(MasterControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Join { get { return m_Wrapper.m_Menu_Join; } }
        public InputAction @Leave { get { return m_Wrapper.m_Menu_Leave; } }
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
                Leave.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnLeave;
                Leave.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnLeave;
                Leave.cancelled -= m_Wrapper.m_MenuActionsCallbackInterface.OnLeave;
            }
            m_Wrapper.m_MenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                Join.started += instance.OnJoin;
                Join.performed += instance.OnJoin;
                Join.cancelled += instance.OnJoin;
                Leave.started += instance.OnLeave;
                Leave.performed += instance.OnLeave;
                Leave.cancelled += instance.OnLeave;
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
}
public interface IMenuActions
{
    void OnJoin(InputAction.CallbackContext context);
    void OnLeave(InputAction.CallbackContext context);
}
