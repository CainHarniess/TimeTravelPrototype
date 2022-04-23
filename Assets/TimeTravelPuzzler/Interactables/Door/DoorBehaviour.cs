using Osiris.EditorCustomisation;
using Osiris.TimeTravelPuzzler.Interactable;
using Osiris.Utilities.Extensions;
using Osiris.Utilities.Logging;
using UnityEngine;
using ILogger = Osiris.Utilities.Logging.ILogger;

namespace Osiris.TimeTravelPuzzler.Interactables.Doors
{
    [ExecuteInEditMode]
    public partial class DoorBehaviour : OsirisMonoBehaviour, IDoor, ILoggableBehaviour
    {
        private SpriteRenderer _sprite;
        private BoxCollider2D _collider;

        [Header(InspectorHeaders.Injections)]
        [SerializeField] private UnityConsoleLogger _Logger;
        [Tooltip(ToolTips.DoorBuildDirector)]
        [SerializeField] private DoorBuildDirectorSO _BuildDirector;
        [SerializeField] private Animator _Animator;
        [SerializeField] private Sprite _OpenSprite;
        [SerializeField] private Sprite _ClosedSprite;

        [Header(InspectorHeaders.DebugVariables)]
        [ReadOnly] [SerializeField] private bool _isOpenInEditMode;
        [Tooltip(ToolTips.Door)]
        [SerializeReference] private IDoor _Door;
        
        public bool IsOpen => _Door.IsOpen;

        public ILogger Logger => _Logger;

        protected override void Awake()
        {
            base.Awake();
            InitialiseDoor();
            this.IsInjectionPresent(_Logger, nameof(_Logger));
            this.AddComponentInjectionIfNotPresent(ref _Animator, nameof(_Animator));

            if (_isOpenInEditMode)
            {
                _Animator.SetBool(AnimationParameters.IsOpen, true);
            }
            else
            {
                _Animator.SetBool(AnimationParameters.IsOpen, false);
            }
        }

        public bool CanOpen()
        {
            return _Door.CanOpen();
        }

        public void Open()
        {
            _Door.Open();
            _Animator.SetTrigger(AnimationParameters.DoorOpening);
        }

        public bool CanClose()
        {
            return _Door.CanClose();
        }

        public void Close()
        {
            _Door.Close();
            _Animator.SetTrigger(AnimationParameters.DoorClosing);
        }

        [ContextMenu("Open door")]
        public void EditorOpen()
        {
            InitialiseDoor();
            _Door.Open();
            _isOpenInEditMode = true;
            _sprite.sprite = _OpenSprite;
        }

        [ContextMenu("Close door")]
        public void EditorClose()
        {
            InitialiseDoor();
            _Door.Close();
            _isOpenInEditMode = false;
            _sprite.sprite = _ClosedSprite;
        }

        private void InitialiseDoor()
        {
            _collider = GetComponent<BoxCollider2D>();
            _sprite = GetComponent<SpriteRenderer>();
            _Door = _BuildDirector.Construct(GameObjectName, _Logger, _sprite, _collider, _isOpenInEditMode);
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
