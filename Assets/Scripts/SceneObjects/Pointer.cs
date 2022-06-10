using UnityEngine;
using Zenject;

namespace SceneObjects
{
    public class Pointer : MonoBehaviour
    {
        public Vector3 Position => transform.position;
        public float Radius;

        private MainCamera _mainCamera;
        
        [Inject]
        private void Construct(MainCamera mainCamera)
        {
            _mainCamera = mainCamera;
        }

        private void LateUpdate()
        {
            transform.position = _mainCamera.GetPointerWorldPosition();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, Radius);
        }
    }
}