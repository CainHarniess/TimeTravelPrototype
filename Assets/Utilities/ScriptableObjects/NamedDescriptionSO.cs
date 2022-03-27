using UnityEngine;

namespace Osiris.Utilities.ScriptableObjects
{
    public class NamedDescriptionSO : DescriptionSO
    {
        [SerializeField] private string _name;

        protected string Name
        {
            get => _name;
            private set
            {
                if (_name == value)
                {
                    return;
                }
                _name = value;
            }
        }

        private void OnAwake()
        {
            if (_name == string.Empty)
            {
                Name = name;
            }
        }

        private void OnValidate()
        {
            if (_name == string.Empty)
            {
                Name = name;
            }
        }
    }
}
