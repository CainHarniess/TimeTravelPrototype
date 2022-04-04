using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.TimeTravelPuzzler.Interactables.Core;
using Osiris.Utilities.Logging;

namespace Osiris.TimeTravelPuzzler.Interactables
{
    public class NewFloorPadPressCommand : IRewindableCommand
    {
        private IWeightedFloorPad _floorPad;
        private int _candidateWeight;
        private ILogger _logger;
        private string _gameObjectName;

        private bool _isReplayExecution;

        public NewFloorPadPressCommand(IWeightedFloorPad floorPad, int candidateWeight, ILogger logger, string gameObjectName)
        {
            _floorPad = floorPad;
            _candidateWeight = candidateWeight;
            _logger = logger;
            _gameObjectName = gameObjectName;
            _isReplayExecution = false;
        }

        public ICommand Inverse => new InverseCommand(_floorPad, _candidateWeight, _logger, _gameObjectName);

        public string Description => "Floor pad press";
        protected IWeightedFloorPad FloorPad => _floorPad;
        protected int CandidateWeight => _candidateWeight;

        public virtual bool CanExecute(object parameter = null)
        {
            if (_isReplayExecution)
            {
                _floorPad.AddWeight(CandidateWeight);
            }
            return _floorPad.CanPress();
        }

        public void Execute(object parameter = null)
        {
            _floorPad.Press();
            _isReplayExecution = true;
        }



        private class InverseCommand : NewFloorPadReleaseCommand
        {
            public InverseCommand(IWeightedFloorPad floorPad, int candidateWeight, ILogger logger, string gameObjectName)
                : base(floorPad, candidateWeight, logger, gameObjectName)
            {

            }
            public override bool CanExecute(object parameter = null)
            {
                FloorPad.RemoveWeight(CandidateWeight);
                return base.CanExecute();
            }
        }
    }
}