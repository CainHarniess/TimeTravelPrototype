using Osiris.EditorCustomisation;
using Osiris.TimeTravelPuzzler.Interactables.Doors.Animations;
using Osiris.Utilities.Animation;
using Osiris.Utilities.DependencyInjection;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Interactables.Doors
{
    [ExecuteInEditMode]
    public partial class DoorBehaviour : LoggableMonoBehaviour, IDoor, IInjectableBehaviour
    {

        [Header(InspectorHeaders.Injections)]
        [SerializeField] private BoxCollider2D _Collider;
        [Tooltip(ToolTips.DoorBuildDirector)]
        [SerializeField] private DoorBuildDirectorSO _BuildDirector;
        [SerializeField] private DoorStateAnimationBehaviour _StateAnimator;
        [SerializeField] private RegularTriggerAnimationBehaviour _FlickerAnimator;
        [SerializeField] private DoorSfxPlayer _SfxPlayer;


        [Header(InspectorHeaders.DebugVariables)]
        [ReadOnly] [SerializeField] private bool _isOpenInEditMode;
        [Tooltip(ToolTips.Door)]
        [SerializeReference] private IDoor _Door;

        public bool IsOpen => _Door.IsOpen;

        protected override void Awake()
        {
            base.Awake();
            InitialiseDoor();

            if (Application.IsPlaying(gameObject))
            {
                _StateAnimator.SetInitialState(IsOpen);
            }

            if (_isOpenInEditMode)
            {
                DisableFlicker();
            }
            else
            {
                EnableFlicker();
            }
        }

        public bool CanOpen()
        {
            return _Door.CanOpen();
        }

        public void Open()
        {
            _Door.Open();
            _StateAnimator.Open();
            _SfxPlayer.OnOpen();
            DisableFlicker();
        }

        public bool CanClose()
        {
            return _Door.CanClose();
        }

        public void Close()
        {
            _Door.Close();
            _StateAnimator.Close();
            _SfxPlayer.OnClose();
            EnableFlicker();
        }

        private void InitialiseDoor()
        {
            this.AddComponentInjectionIfNotPresent(ref _Collider, nameof(_Collider));
            this.AddComponentInjectionIfNotPresent(ref _StateAnimator, nameof(_StateAnimator));
            this.AddComponentInjectionIfNotPresent(ref _FlickerAnimator, nameof(_FlickerAnimator));
            this.AddComponentInjectionIfNotPresent(ref _SfxPlayer, nameof(_SfxPlayer));

            _Door = _BuildDirector.Construct(GameObjectName, Logger, _Collider, _isOpenInEditMode);
        }

        private void EnableFlicker()
        {
            _FlickerAnimator.enabled = true;
        }

        private void DisableFlicker()
        {
            _FlickerAnimator.enabled = false;
        }



        [ContextMenu("Open (Edit Mode)")]
        public void OpenInEditMode()
        {
            InitialiseDoor();
            _Door.Open();
            _StateAnimator.SetInitialState(true);
            _isOpenInEditMode = true;
        }

        [ContextMenu("Close (Edit Mode)")]
        public void CloseInEditMode()
        {
            InitialiseDoor();
            _Door.Close();
            _StateAnimator.SetInitialState(false);
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
