using System;
using UnityEngine;

namespace Osiris.Utilities.References
{
    [Serializable]
    [CreateAssetMenu(fileName = AssetMenu.BoolReferenceFileName, menuName = AssetMenu.BoolReferencePath)]
    public class BoolReference : GenericReference<bool>
    {

    }
}
