using Osiris.Testing;
using Osiris.Testing.Abstractions;
using Osiris.TimeTravelPuzzler.Interactables.Doors;
using Osiris.Utilities.ScriptableObjects;
using System;
using UnityEngine;
using ILogger = Osiris.Utilities.Logging.ILogger;

namespace Osiris.TimeTravelPuzzler.Interactables
{
    [CreateAssetMenu(fileName = DoorAssetMenu.DoorBuilderFileName, menuName = DoorAssetMenu.DoorBuilderPath)]
    public class DoorBuilderSO : DescriptionSO
    {
        public string GameObjectName { get; private set; }
        public DoorBuilderSO WithGameObjectName(string gameObjectName)
        {
            GameObjectName = gameObjectName;
            return this;
        }

        public ILogger Logger { get; private set; }
        public DoorBuilderSO WithLogger(ILogger logger)
        {
            Logger = logger;
            return this;
        }

        public SpriteRenderer SpriteRenderer { get; private set; }
        public DoorBuilderSO WithSpriteRenderer(SpriteRenderer spriteRenderer)
        {
            SpriteRenderer = spriteRenderer;
            return this;
        }

        public BoxCollider2D BoxCollider2D { get; private set; }
        public DoorBuilderSO WithBoxCollider2D(BoxCollider2D boxCollider2D)
        {
            BoxCollider2D = boxCollider2D;
            return this;
        }

        public bool IsOpen { get; private set; }
        internal DoorBuilderSO WithStatus(bool isOpen)
        {
            IsOpen = isOpen;
            return this;
        }

        public IDoor Build()
        {
            ISpriteRendererProxy rendererProxy = new SpriteRendererProxy(SpriteRenderer);
            IBehaviourProxy colliderProxy = new BehaviourProxy(BoxCollider2D);

            return new Door(GameObjectName, Logger, rendererProxy, colliderProxy, IsOpen);
        }
    }
}
