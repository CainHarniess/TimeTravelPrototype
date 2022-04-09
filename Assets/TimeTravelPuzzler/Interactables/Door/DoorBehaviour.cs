using Osiris.EditorCustomisation;
using Osiris.Utilities.Logging;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Interactables.Doors
{
    [ExecuteInEditMode]
    public class DoorBehaviour : MonoBehaviour, IDoor
    {
        private string _gameObjectName;
        private SpriteRenderer _sprite;
        private BoxCollider2D _collider;

        [Header(InspectorHeaders.ControlVariables)]
        [Tooltip(ToolTips.DoorBuildDirector)]
        [SerializeField] private DoorBuildDirectorSO _BuildDirector;

        [Header(InspectorHeaders.DebugVariables)]
        [ReadOnly] [SerializeField] private bool _isOpenInEditMode;
        [Tooltip(ILoggerToolTips.ToolTip)]
        [SerializeField] private UnityConsoleLogger _Logger;
        [Tooltip(ToolTips.Door)]
        [SerializeReference] private IDoor _Door;

        private string GameObjectName
        {
            get
            {
                if (_gameObjectName == null)
                {
                    _gameObjectName = gameObject.name;
                }
                return _gameObjectName;
            }
        }

        private void Awake()
        {
            InitialiseDoor();
        }


        public bool IsOpen => _Door.IsOpen;

        public bool CanOpen()
        {
            return _Door.CanOpen();
        }

        public void Open()
        {
            _Door.Open();
        }

        public bool CanClose()
        {
            return _Door.CanClose();
        }

        public void Close()
        {
            _Door.Close();
        }

        [ContextMenu("Open door")]
        public void EditorOpen()
        {
            InitialiseDoor();
            _Door.Open();
            _isOpenInEditMode = true;
        }

        [ContextMenu("Close door")]
        public void EditorClose()
        {
            InitialiseDoor();
            _Door.Close();
            _isOpenInEditMode = false;
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
