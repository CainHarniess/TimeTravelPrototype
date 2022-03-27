using UnityEngine;

namespace Osiris.Utilities.ScriptableObjects
{
    public class DescriptionSO : ScriptableObject
    {
        [TextArea] [SerializeField] private string _description;

        protected string Description => _description;
    }
}
