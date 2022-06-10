using UnityEngine;

namespace SceneObjects
{
    [RequireComponent(typeof(Camera))]
    public class MainCamera : MonoBehaviour
    {
        public Camera Camera
        {
            get
            {
                if (_camera == null)
                {
                    _camera = GetComponent<Camera>();
                }

                return _camera;
            }
        }
        private Camera _camera;
        

        public Vector3 GetPointerWorldPosition()
        {
            var pointerPosition = Camera.ScreenToWorldPoint(Input.mousePosition);
            pointerPosition.z = 0;
            
            return pointerPosition;
        }
    }
}