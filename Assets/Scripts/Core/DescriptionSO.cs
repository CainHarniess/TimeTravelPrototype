using UnityEngine;

namespace Osiris.TimeTravelPuzzler
{
    public class DescriptionSO : ScriptableObject
    {
        [TextArea] [SerializeField] private string _description;
    }
}
