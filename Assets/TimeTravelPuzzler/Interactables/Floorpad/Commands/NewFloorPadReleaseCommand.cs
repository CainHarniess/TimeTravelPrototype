using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.TimeTravelPuzzler.Interactables.Core;
using Osiris.Utilities.Logging;

namespace Osiris.TimeTravelPuzzler.Interactables
{

    public class NewFloorPadReleaseCommand : IRewindableCommand
    {
        private IWeightedFloorPad _floorPad;
        private int _candidateWeight;
        private ILogger _logger;
        private string _gameObjectName;

        private bool _isReplayExecution;

        public NewFloorPadReleaseCommand(IWeightedFloorPad floorPad, int candidateWeight, ILogger logger, string gameObjectName)
        {
            _floorPad = floorPad;
            _candidateWeight = candidateWeight;
            _logger = logger;
            _gameObjectName = gameObjectName;
            _isReplayExecution = false;
        }

        public ICommand Inverse => new InverseCommand(_floorPad, _candidateWeight, _logger, _gameObjectName);
        public string Description => "Floor pad release";
        protected IWeightedFloorPad FloorPad => _floorPad;
        protected int CandidateWeight=> _candidateWeight;

        public virtual bool CanExecute(object parameter = null)
        {
            _floorPad.RemoveWeight(CandidateWeight);
            return _floorPad.CanRelease();
        }

        public void Execute(object parameter = null)
        {
            _floorPad.Release();
            _isReplayExecution = true;
        }



        private class InverseCommand : NewFloorPadPressCommand
        {
            public InverseCommand(IWeightedFloorPad floorPad, int candidateWeight, ILogger logger,
                                                 string gameObjectName)
                : base(floorPad, candidateWeight, logger, gameObjectName)
            {

            }

            public override bool CanExecute(object parameter = null)
            {
                FloorPad.AddWeight(CandidateWeight);
                return base.CanExecute();
            }
        }
    }
}