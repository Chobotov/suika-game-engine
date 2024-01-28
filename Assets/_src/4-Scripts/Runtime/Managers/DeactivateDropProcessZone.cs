using UnityEngine;

namespace SGEngine.DropItem
{
    public class DeactivateDropProcessZone : MonoBehaviour
    {
        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.TryGetComponent<DropItem>(out var dropItem)) return;

            if (!dropItem.IsDropProcess) return;

            dropItem.IsDropProcess = false;
        }
    }
}