using Osiris.EditorCustomisation;
using Osiris.Utilities.DependencyInjection;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Interactables
{
    public class LightBehaviour : OsirisMonoBehaviour, IInjectableBehaviour
    {
        [Header(InspectorHeaders.Injections)]
        [SerializeField] private SpriteRenderer _SpriteRenderer;
        [SerializeField] private Sprite _OnSprite;
        [SerializeField] private Sprite _OffSprite;

        [Header(InspectorHeaders.DebugVariables)]
        [ReadOnly] [SerializeField] private bool _IsOn;


        protected override void Awake()
        {
            base.Awake();
            this.AddComponentInjectionIfNotPresent(ref _SpriteRenderer, nameof(_SpriteRenderer));
        }

        [ContextMenu("Switch On")]
        public void SwitchOn()
        {
            if (_IsOn)
            {
                return;
            }
            _IsOn = true;
            _SpriteRenderer.sprite = _OnSprite;
        }

        [ContextMenu("Switch Off")]
        public void SwitchOff()
        {
            if (!_IsOn)
            {
                return;
            }
            _IsOn = false;
            _SpriteRenderer.sprite = _OffSprite;
        }
    }
}
