using Osiris.EditorCustomisation;
using System;
using UnityEngine;
using OUL = Osiris.Utilities.Logging;

namespace Osiris.TimeTravelPuzzler.Interactables
{
    [Serializable]
    public class Door : IDoor
    {
        private readonly string _gameObjectName;
        private readonly OUL.ILogger _logger;

        [ReadOnly] [SerializeField] private bool _IsOpen;

        public Door(string gameObjectName, OUL.ILogger logger)
        {
            _gameObjectName = gameObjectName;
            _logger = logger;
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
        }
    }
}
