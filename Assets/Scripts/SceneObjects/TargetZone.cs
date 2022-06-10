using UnityEngine;

namespace SceneObjects
{
    public class TargetZone : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Cockroach cockroach))
            {
                cockroach.Dispose();
            }
        }
    }
}