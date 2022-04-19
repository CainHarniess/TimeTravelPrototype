using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.Utilities.Commands;

namespace Osiris.TimeTravelPuzzler.Core
{
    public abstract class RewindableCommandBuilder : CommandBuilderSO
    {
        public string CommandDescription { get; private set; }
        public RewindableCommandBuilder WithCommandDescription(string commandDescription)
        {
            CommandDescription = commandDescription;
            return this;
        }

        public ICommand Inverse { get; private set; }
        public RewindableCommandBuilder WithInverse(ICommand inverse)
        {
            Inverse = inverse;
            return this;
        }
    }
}
