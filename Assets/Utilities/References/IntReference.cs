using System;
using UnityEngine;

namespace Osiris.Utilities.References
{

    [Serializable]
    [CreateAssetMenu(fileName = AssetMenu.IntReferenceFileName, menuName = AssetMenu.IntReferencePath)]

    public class IntReference : GenericReference<int>
    {

    }
}
