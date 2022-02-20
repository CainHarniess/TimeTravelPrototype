using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Core
{
    public class DescriptionSO : ScriptableObject
    {
        [TextArea] [SerializeField] private string _description;

        protected string Description { get => _description; }
    }
}
