using Osiris.Utilities.ScriptableObjects;
using UnityEngine;

namespace Osiris.Utilities.Audio
{
    [CreateAssetMenu(fileName = AssetMenu.AudioClipDataFileName, menuName = AssetMenu.AudioClipDataPath)]
    public class AudioClipData : DescriptionSO
    {
        [SerializeField] private AudioClip _Clip;

        public AudioClip Clip { get => _Clip; }
    }
}
