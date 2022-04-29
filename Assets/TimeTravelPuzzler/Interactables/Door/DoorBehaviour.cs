using Osiris.EditorCustomisation;
using Osiris.Utilities.Extensions;
using Osiris.Utilities.Logging;
using UnityEngine;
using ILogger = Osiris.Utilities.Logging.ILogger;

namespace Osiris.TimeTravelPuzzler.Interactables.Doors
{
    [ExecuteInEditMode]
    public partial class DoorBehaviour : OsirisMonoBehaviour, IDoor, ILoggableBehaviour
    {
        private BoxCollider2D _collider;
        [SerializeField] private StateAnimationBehaviour _stateAnimator;
        private FlickerAnimationBehaviour _flickerAnimator;

        [Header(InspectorHeaders.Injections)]
        [SerializeField] private UnityConsoleLogger _Logger;
        [Tooltip(ToolTips.DoorBuildDirector)]
        [SerializeField] private DoorBuildDirectorSO _BuildDirector;

        [Header(InspectorHeaders.DebugVariables)]
        [ReadOnly] [SerializeField] private bool _isOpenInEditMode;
        [Tooltip(ToolTips.Door)]
        [SerializeReference] private IDoor _Door;

        public bool IsOpen => _Door.IsOpen;

        public ILogger Logger => _Logger;

        protected override void Awake()
        {
            base.Awake();
            this.IsInjectionPresent(_Logger, nameof(_Logger));
            InitialiseDoor();

            if (Application.IsPlaying(gameObject))
            {
                _stateAnimator.SetInitialState(IsOpen);
            }
        }

        public bool CanOpen()
        {
            return _Door.CanOpen();
        }

        public void Open()
        {
            _Door.Open();
            _stateAnimator.Open();
            DisableFlicker();
        }

        public bool CanClose()
        {
            return _Door.CanClose();
        }

        public void Close()
        {
            _Door.Close();
            _stateAnimator.Close();
            EnableFlicker();
        }

        private void InitialiseDoor()
        {
            _collider = GetComponent<BoxCollider2D>();
            _stateAnimator = GetComponent<StateAnimationBehaviour>();
            _flickerAnimator = GetComponent<FlickerAnimationBehaviour>();

            _Door = _BuildDirector.Construct(GameObjectName, _Logger, _collider, _isOpenInEditMode);
        }

        private void EnableFlicker()
        {
            _flickerAnimator.enabled = true;
        }

        private void DisableFlicker()
        {
            _flickerAnimator.enabled = false;
        }



        [ContextMenu("Open (Edit Mode)")]
        public void OpenInEditMode()
        {
            InitialiseDoor();
            _Door.Open();
            _stateAnimator.SetInitialState(true);
            _isOpenInEditMode = true;
        }

        [ContextMenu("Close (Edit Mode)")]
        public void CloseInEditMode()
        {
            InitialiseDoor();
            _Door.Close();
            _stateAnimator.SetInitialState(false);
            _isOpenInEditMode = false;
        }




        private struct ToolTips
        {
            public const string DoorBuildDirector = "The instance of the DoorBuildDirectorSO class responsible for instantiating the nested CLR door."
                                                    + "\n"
                                                    + "\n"
                                                    + "The DoorBehaviour class will not function correctly if this is not populated at run time.";

            public const string Door = "The nested CLR object representing the door containing the core logic."
                                       + "\n"
                                       + "\n"
                                       + "This field is intentionally read-only.\n"
                                       + "Use the context menu commands to manually change the state of the door.";
        }
    }
}
