using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Collectibles
{
    public class Collector : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<ICollectible>() is ICollectible collectible)
            {
                collectible.Collect();
            }
        }
    }
}
