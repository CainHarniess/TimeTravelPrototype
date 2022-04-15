using Osiris.Utilities.ScriptableObjects;
using System;

namespace Osiris.Utilities.Commands
{
    public abstract class CommandBuilderSO : DescriptionSO
    {
        public Func<bool> CanExecute { get; protected set; }
        public CommandBuilderSO WithCanExecute<T>(Func<bool> candidateWeight)
        {
            CanExecute = candidateWeight;
            return this;
        }

        public Action Execute { get; protected set; }
        public CommandBuilderSO WithExecute(Action execute)
        {
            Execute = execute;
            return this;
        }
    }
}
