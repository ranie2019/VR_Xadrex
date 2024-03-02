using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

[AddComponentMenu("XR/Action Based Controller Manager")]
[DefaultExecutionOrder(kControllerManagerUpdateOrder)]
public class ActionBasedControllerManager : MonoBehaviour
{
    public const int kControllerManagerUpdateOrder = 10;

    public enum StateId
    {
        None,
        Select,
        Teleport,
        Interact,
    }

    [Serializable]
    public class StateEnterEvent : UnityEvent<StateId> { }

    [Serializable]
    public class StateUpdateEvent : UnityEvent { }

    [Serializable]
    public class StateExitEvent : UnityEvent<StateId> { }

    [Serializable]
    public class ControllerState
    {
        [SerializeField]
        [Tooltip("Sets the controller state to be active. " +
                 "For the default states, setting this value to true will automatically update their StateUpdateEvent.")]
        bool m_Enabled;
        public bool enabled
        {
            get => m_Enabled;
            set => m_Enabled = value;
        }

        [SerializeField]
        [HideInInspector]
        StateId m_Id;
        public StateId id
        {
            get => m_Id;
            set => m_Id = value;
        }

        [SerializeField]
        StateEnterEvent m_OnEnter = new StateEnterEvent();
        public StateEnterEvent onEnter
        {
            get => m_OnEnter;
            set => m_OnEnter = value;
        }

        [SerializeField]
        StateUpdateEvent m_OnUpdate = new StateUpdateEvent();
        public StateUpdateEvent onUpdate
        {
            get => m_OnUpdate;
            set => m_OnUpdate = value;
        }

        [SerializeField]
        StateExitEvent m_OnExit = new StateExitEvent();
        public StateExitEvent onExit
        {
            get => m_OnExit;
            set => m_OnExit = value;
        }

        public ControllerState(StateId defaultId = StateId.None) => this.id = defaultId;
    }

    [Space]
    [Header("Controller GameObjects")]

    [SerializeField, FormerlySerializedAs("m_BaseControllerGO")]
    GameObject m_BaseControllerGameObject;
    public GameObject baseControllerGameObject
    {
        get => m_BaseControllerGameObject;
        set => m_BaseControllerGameObject = value;
    }

    [SerializeField, FormerlySerializedAs("m_TeleportControllerGO")]
    GameObject m_TeleportControllerGameObject;
    public GameObject teleportControllerGameObject
    {
        get => m_TeleportControllerGameObject;
        set => m_TeleportControllerGameObject = value;
    }

    [Space]
    [Header("Controller Actions")]

    [SerializeField]
    InputActionReference m_TeleportModeActivate;
    public InputActionReference teleportModeActivate
    {
        get => m_TeleportModeActivate;
        set => m_TeleportModeActivate = value;
    }

    [SerializeField]
    InputActionReference m_TeleportModeCancel;
    public InputActionReference teleportModeCancel
    {
        get => m_TeleportModeCancel;
        set => m_TeleportModeCancel = value;
    }

    [SerializeField]
    InputActionReference m_Turn;
    public InputActionReference turn
    {
        get => m_Turn;
        set => m_Turn = value;
    }

    [SerializeField]
    InputActionReference m_Move;
    public InputActionReference move
    {
        get => m_Move;
        set => m_Move = value;
    }

    [SerializeField, FormerlySerializedAs("m_TranslateObject")]
    InputActionReference m_TranslateAnchor;
    public InputActionReference translateAnchor
    {
        get => m_TranslateAnchor;
        set => m_TranslateAnchor = value;
    }

    [SerializeField, FormerlySerializedAs("m_RotateObject")]
    InputActionReference m_RotateAnchor;
    public InputActionReference rotateAnchor
    {
        get => m_RotateAnchor;
        set => m_RotateAnchor = value;
    }

    [Space]
    [Header("Default States")]

    [SerializeField]
    ControllerState m_SelectState = new ControllerState(StateId.Select);
    public ControllerState selectState => m_SelectState;

    [SerializeField]
    ControllerState m_TeleportState = new ControllerState(StateId.Teleport);
    public ControllerState teleportState => m_TeleportState;

    [SerializeField]
    ControllerState m_InteractState = new ControllerState(StateId.Interact);
    public ControllerState interactState => m_InteractState;

    readonly List<ControllerState> m_DefaultStates = new List<ControllerState>();
    XRBaseController m_BaseController;
    XRBaseInteractor m_BaseInteractor;
    XRInteractorLineVisual m_BaseLineVisual;

    XRBaseController m_TeleportController;
    XRBaseInteractor m_TeleportInteractor;
    XRInteractorLineVisual m_TeleportLineVisual;

    protected void OnEnable()
    {
        FindBaseControllerComponents();
        FindTeleportControllerComponents();

        m_SelectState.onEnter.AddListener(OnEnterSelectState);
        m_SelectState.onUpdate.AddListener(OnUpdateSelectState);
        m_SelectState.onExit.AddListener(OnExitSelectState);

        m_TeleportState.onEnter.AddListener(OnEnterTeleportState);
        m_TeleportState.onUpdate.AddListener(OnUpdateTeleportState);
        m_TeleportState.onExit.AddListener(OnExitTeleportState);

        m_InteractState.onEnter.AddListener(OnEnterInteractState);
        m_InteractState.onUpdate.AddListener(OnUpdateInteractState);
        m_InteractState.onExit.AddListener(OnExitInteractState);
    }

    protected void OnDisable()
    {
        m_SelectState.onEnter.RemoveListener(OnEnterSelectState);
        m_SelectState.onUpdate.RemoveListener(OnUpdateSelectState);
        m_SelectState.onExit.RemoveListener(OnExitSelectState);

        m_TeleportState.onEnter.RemoveListener(OnEnterTeleportState);
        m_TeleportState.onUpdate.RemoveListener(OnUpdateTeleportState);
        m_TeleportState.onExit.RemoveListener(OnExitTeleportState);

        m_InteractState.onEnter.RemoveListener(OnEnterInteractState);
        m_InteractState.onUpdate.RemoveListener(OnUpdateInteractState);
        m_InteractState.onExit.RemoveListener(OnExitInteractState);
    }

    protected void Start()
    {
        m_DefaultStates.Add(m_SelectState);
        m_DefaultStates.Add(m_TeleportState);
        m_DefaultStates.Add(m_InteractState);

        TransitionState(null, m_SelectState);
    }

    protected void Update()
    {
        foreach (var state in m_DefaultStates)
        {
            if (state.enabled)
            {
                state.onUpdate.Invoke();
                return;
            }
        }
    }

    void TransitionState(ControllerState fromState, ControllerState toState)
    {
        if (fromState != null)
        {
            fromState.enabled = false;
            fromState.onExit.Invoke(toState?.id ?? StateId.None);
        }

        if (toState != null)
        {
            toState.onEnter.Invoke(fromState?.id ?? StateId.None);
            toState.enabled = true;
        }
    }

    void FindBaseControllerComponents()
    {
        if (m_BaseControllerGameObject == null)
        {
            Debug.LogWarning("Missing reference to Base Controller GameObject.", this);
            return;
        }

        if (m_BaseController == null)
        {
            m_BaseController = m_BaseControllerGameObject.GetComponent<XRBaseController>();
            if (m_BaseController == null)
                Debug.LogWarning($"Cannot find any {nameof(XRBaseController)} component on the Base Controller GameObject.", this);
        }

        if (m_BaseInteractor == null)
        {
            m_BaseInteractor = m_BaseControllerGameObject.GetComponent<XRBaseInteractor>();
            if (m_BaseInteractor == null)
                Debug.LogWarning($"Cannot find any {nameof(XRBaseInteractor)} component on the Base Controller GameObject.", this);
        }

        if (m_BaseInteractor is XRRayInteractor && m_BaseLineVisual == null)
        {
            m_BaseLineVisual = m_BaseControllerGameObject.GetComponent<XRInteractorLineVisual>();
            if (m_BaseLineVisual == null)
                Debug.LogWarning($"Cannot find any {nameof(XRInteractorLineVisual)} component on the Base Controller GameObject.", this);
        }
    }

    void FindTeleportControllerComponents()
    {
        if (m_TeleportControllerGameObject == null)
        {
            Debug.LogWarning("Missing reference to the Teleport Controller GameObject.", this);
            return;
        }

        if (m_TeleportController == null)
        {
            m_TeleportController = m_TeleportControllerGameObject.GetComponent<XRBaseController>();
            if (m_TeleportController == null)
                Debug.LogWarning($"Cannot find {nameof(XRBaseController)} component on the Teleport Controller GameObject.", this);
        }

        if (m_TeleportLineVisual == null)
        {
            m_TeleportLineVisual = m_TeleportControllerGameObject.GetComponent<XRInteractorLineVisual>();
            if (m_TeleportLineVisual == null)
                Debug.LogWarning($"Cannot find {nameof(XRInteractorLineVisual)} component on the Teleport Controller GameObject.", this);
        }

        if (m_TeleportInteractor == null)
        {
            m_TeleportInteractor = m_TeleportControllerGameObject.GetComponent<XRRayInteractor>();
            if (m_TeleportInteractor == null)
                Debug.LogWarning($"Cannot find {nameof(XRRayInteractor)} component on the Teleport Controller GameObject.", this);
        }
    }

    void SetBaseController(bool enable)
    {
        FindBaseControllerComponents();

        if (m_BaseController != null)
            m_BaseController.enableInputActions = enable;

        if (m_BaseInteractor != null)
            m_BaseInteractor.enabled = enable;

        if (m_BaseInteractor is XRRayInteractor && m_BaseLineVisual != null)
            m_BaseLineVisual.enabled = enable;
    }

    void SetTeleportController(bool enable)
    {
        FindTeleportControllerComponents();

        if (m_TeleportLineVisual != null)
            m_TeleportLineVisual.enabled = enable;

        if (m_TeleportController != null)
            m_TeleportController.enableInputActions = enable;

        if (m_TeleportInteractor != null)
            m_TeleportInteractor.enabled = enable;
    }

    void OnEnterSelectState(StateId previousStateId)
    {
        switch (previousStateId)
        {
            case StateId.None:
                EnableAction(m_TeleportModeActivate);
                EnableAction(m_TeleportModeCancel);
                EnableAction(m_Turn);
                EnableAction(m_Move);
                SetBaseController(true);
                break;
            case StateId.Select:
                break;
            case StateId.Teleport:
                EnableAction(m_Turn);
                EnableAction(m_Move);
                SetBaseController(true);
                break;
            case StateId.Interact:
                EnableAction(m_Turn);
                EnableAction(m_Move);
                break;
            default:
                Debug.Assert(false, $"Unhandled case when entering Select from {previousStateId}.");
                break;
        }
    }

    void OnExitSelectState(StateId nextStateId)
    {
        switch (nextStateId)
        {
            case StateId.None:
                break;
            case StateId.Select:
                break;
            case StateId.Teleport:
                DisableAction(m_Turn);
                DisableAction(m_Move);
                SetBaseController(false);
                break;
            case StateId.Interact:
                DisableAction(m_Turn);
                DisableAction(m_Move);
                break;
            default:
                Debug.Assert(false, $"Unhandled case when exiting Select to {nextStateId}.");
                break;
        }
    }

    void OnEnterTeleportState(StateId previousStateId) => SetTeleportController(true);

    void OnExitTeleportState(StateId nextStateId) => SetTeleportController(false);

    void OnEnterInteractState(StateId previousStateId)
    {
        EnableAction(m_TranslateAnchor);
        EnableAction(m_RotateAnchor);
    }

    void OnExitInteractState(StateId nextStateId)
    {
        DisableAction(m_TranslateAnchor);
        DisableAction(m_RotateAnchor);
    }

    void OnUpdateSelectState()
    {
        var teleportModeAction = GetInputAction(m_TeleportModeActivate);
        var cancelTeleportModeAction = GetInputAction(m_TeleportModeCancel);

        var triggerTeleportMode = teleportModeAction != null && teleportModeAction.triggered;
        var cancelTeleport = cancelTeleportModeAction != null && cancelTeleportModeAction.triggered;

        if (triggerTeleportMode && !cancelTeleport)
        {
            TransitionState(m_SelectState, m_TeleportState);
            return;
        }

        FindBaseControllerComponents();

        if (m_BaseInteractor.selectTarget != null)
            TransitionState(m_SelectState, m_InteractState);
    }

    void OnUpdateTeleportState()
    {
        var teleportModeAction = GetInputAction(m_TeleportModeActivate);
        var cancelTeleportModeAction = GetInputAction(m_TeleportModeCancel);

        var cancelTeleport = cancelTeleportModeAction != null && cancelTeleportModeAction.triggered;
        var releasedTeleport = teleportModeAction != null && teleportModeAction.phase == InputActionPhase.Waiting;

        if (cancelTeleport || releasedTeleport)
            TransitionState(m_TeleportState, m_SelectState);
    }

    void OnUpdateInteractState()
    {
        if (m_BaseInteractor.selectTarget == null)
            TransitionState(m_InteractState, m_SelectState);
    }

    static void EnableAction(InputActionReference actionReference)
    {
        var action = GetInputAction(actionReference);
        if (action != null && !action.enabled)
            action.Enable();
    }

    static void DisableAction(InputActionReference actionReference)
    {
        var action = GetInputAction(actionReference);
        if (action != null && action.enabled)
            action.Disable();
    }

    static InputAction GetInputAction(InputActionReference actionReference)
    {
        return actionReference != null ? actionReference.action : null;
    }
}
