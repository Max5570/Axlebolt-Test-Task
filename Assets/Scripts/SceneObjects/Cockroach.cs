using System;
using UnityEngine;
using Zenject;

namespace SceneObjects
{
    public class Cockroach : MonoBehaviour, IPoolable<Vector2, IMemoryPool>, IDisposable
    {
        [SerializeField] private float _speed;
        
        public Action OnDespawn;

        private TargetZone _targetZone;
        private Pointer _pointer;
        private IMemoryPool _pool;

        [Inject]
        private void Construct(Pointer pointer, TargetZone targetZone)
        {
            _pointer = pointer;
            _targetZone = targetZone;
        }
        
        private void FixedUpdate()
        {
            var pointerPosition = _pointer.Position;
            var distance = Vector3.Distance(pointerPosition, transform.position);

            var direction = distance < _pointer.Radius
                ? (transform.position - pointerPosition).normalized
                : (_targetZone.transform.position - transform.position).normalized;

            transform.position += direction * _speed * Time.fixedDeltaTime;
        }

        public void OnSpawned(Vector2 initialPosition, IMemoryPool pool)
        {
            _pool = pool;
            transform.position = initialPosition;
        }
        
        public void OnDespawned()
        {
            _pool = null;
            OnDespawn?.Invoke();
        }

        public void Dispose()
        {
            _pool?.Despawn(this);
        }
        
        public class Factory : PlaceholderFactory<Vector2, Cockroach> { }
    }
}