using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.TimeTravelPuzzler.Interactables.FloorPads.Commands;
using Osiris.Utilities.Commands;
using System;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Interactables
{
    [CreateAssetMenu(fileName = AssetMenu.CommandBuilderFileName, menuName = AssetMenu.CommandBuilderPath)]
    public class FloorPadCommandBuilderSO : ScriptableObject
    {
        public int CandidateWeight { get; private set; }
        public FloorPadCommandBuilderSO WithCandidateWeight(int candidateWeight)
        {
            CandidateWeight = candidateWeight;
            return this;
        }

        public Func<bool> CanExecute { get; protected set; }
        public FloorPadCommandBuilderSO WithCanExecute(Func<bool> candidateWeight)
        {
            CanExecute = candidateWeight;
            return this;
        }

        public Action Execute { get; private set; }
        public FloorPadCommandBuilderSO WithExecute(Action execute)
        {
            Execute = execute;
            return this;
        }

        public Action<int> AdjustWeight { get; private set; }
        public FloorPadCommandBuilderSO WithAdjustWeight(Action<int> adjustWeight)
        {
            AdjustWeight = adjustWeight;
            return this;
        }
        public string CommandDescription { get; private set; }
        public FloorPadCommandBuilderSO WithCommandDescription(string commandDescription)
        {
            CommandDescription = commandDescription;
            return this;
        }

        public ICommand Inverse { get; private set; }
        public FloorPadCommandBuilderSO WithInverse(ICommand inverse)
        {
            Inverse = inverse;
            return this;
        }

        public IRewindableCommand Build()
        {
            return new FloorPadDelegateCommand(CandidateWeight, CanExecute, Execute, AdjustWeight, CommandDescription,
                                               Inverse);
        }

        public void Reset()
        {
            CandidateWeight = 0;
            CanExecute = null;
            Execute = null;
            AdjustWeight = null;
            CommandDescription = null;
            Inverse = null;
        }
    }
}
