using Osiris.EditorCustomisation;
using Osiris.TimeTravelPuzzler.Core;
using Osiris.Utilities.Events;
using Osiris.Utilities.Logging;
using System.Collections.Generic;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Collectibles
{
    public class CollectibleManger : LoggableMonoBehaviour
    {
        [Header(InspectorHeaders.DebugVariables)]
        [ReadOnly] [SerializeField] private int _CollectibleCount = 0;

        [Header(InspectorHeaders.BroadcastsOn)]
        [SerializeField] private EventChannelSO _AllCollected;

        [Header(InspectorHeaders.ListensTo)]
        [SerializeField] private EventChannelSO _Collected;

        private void Start()
        {
            GetCollectibleCount();
        }

        private void OnEnable()
        {
            _Collected.Event += OnCollectibleCollected;
        }

        private void OnCollectibleCollected()
        {
            _CollectibleCount--;

            if (_CollectibleCount <= 0)
            {
                Logger.Log("All collectibles collected.", GameObjectName);
                _AllCollected.Raise();
                return;
            }
        }

        private void GetCollectibleCount()
        {
            var collectibleGos = GameObject.FindGameObjectsWithTag(Tags.Collectible);

            var collectibles = new List<ICollectible>(collectibleGos.Length);
            foreach (var collectibleGo in collectibleGos)
            {
                var collectible = collectibleGo.GetComponent<ICollectible>();

                if (collectible == null)
                {
                    Logger.Log(Messages.MissingCollectibleComponentError, collectibleGo.name, LogLevel.Error);
                    continue;
                }

                collectibles.Add(collectible);
            }
            _CollectibleCount = collectibles.Count;
        }

        private void OnDisable()
        {
            _Collected.Event -= OnCollectibleCollected;
        }





        private struct Messages
        {
            public const string MissingCollectibleComponentError = "Game object with collectible tag missing component implementing ICollectible interface.";
        }
    }
}
