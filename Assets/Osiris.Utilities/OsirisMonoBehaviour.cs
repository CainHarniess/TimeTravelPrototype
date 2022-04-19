using UnityEngine;

namespace Osiris
{
    public abstract class OsirisMonoBehaviour : MonoBehaviour
    {
        private GameObject _cachedGameObject;
        private string _cachedGameObjectName;
        public GameObject GameObject => _cachedGameObject;
        public string GameObjectName => _cachedGameObjectName;

        protected virtual void Awake()
        {
            _cachedGameObject = gameObject;
            _cachedGameObjectName = _cachedGameObject.name;
        }
    }
}
