using Osiris.EditorCustomisation;
using Osiris.Testing;
using System;
using UnityEngine;
using ILogger = Osiris.Utilities.Logging.ILogger;

namespace Osiris.TimeTravelPuzzler.Interactables.Doors
{
    [Serializable]
    public class Door : IDoor
    {
        private readonly string _gameObjectName;
        private readonly ILogger _logger;
        private readonly IRendererProxy _rendererProxy;
        private readonly IBehaviourProxy _colliderProxy;

        [ReadOnly] [SerializeField] private bool _IsOpen;

        public Door(string gameObjectName, ILogger logger, IRendererProxy rendererProxy, IBehaviourProxy colliderProxy, bool isOpen)
        {
            _gameObjectName = gameObjectName;
            _logger = logger;
            _rendererProxy = rendererProxy;
            _colliderProxy = colliderProxy;
            _IsOpen = isOpen;
        }

        public bool IsOpen => _IsOpen; 

        public bool CanOpen()
        {
            if (_IsOpen)
            {
                _logger.Log("Door is already open.", _gameObjectName);
                return false;
            }
            _logger.Log("Open request approved.", _gameObjectName);
            return true;
        }

        public void Open()
        {
            _IsOpen = true;
            SetComponentStatus(false);
        }

        public bool CanClose()
        {
            if (!_IsOpen)
            {
                _logger.Log("Door is already closed.", _gameObjectName);
                return false;
            }
            _logger.Log("Close request approved.", _gameObjectName);
            return true;
        }

        public void Close()
        {
            _IsOpen = false;
            SetComponentStatus(true);
        }

        private void SetComponentStatus(bool isActive)
        {
            _rendererProxy.Enabled = isActive;
            _colliderProxy.Enabled = isActive;
        }
    }
}
