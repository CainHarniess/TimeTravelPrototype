using UnityEngine;

namespace Osiris.Utilities.Extensions
{
    public interface IMonoBehaviour
    {
        GameObject GameObject { get; }
        string GameObjectName { get; }
    }
}
