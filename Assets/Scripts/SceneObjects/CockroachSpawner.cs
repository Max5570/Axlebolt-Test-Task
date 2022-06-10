using System;
using Zenject;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace SceneObjects
{
    public class CockroachSpawner : MonoBehaviour
    {
        [SerializeField] private float _spawnRadius;
        
        public Action OnAllCockroachesDespawned;

        private Cockroach.Factory _cockroachFactory;
        private List<Cockroach> _aliveCockroaches = new List<Cockroach>();
        
        [Inject]
        private void Construct(Cockroach.Factory cockroachFactory)
        {
            _cockroachFactory = cockroachFactory;
        }

        public void Spawn(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var spawnPosition = (Vector2)transform.position + Random.insideUnitCircle * _spawnRadius;
                var cockroach = _cockroachFactory.Create(spawnPosition);
                _aliveCockroaches.Add(cockroach);

                void OnDespawn()
                {
                    cockroach.OnDespawn -= OnDespawn;
                    _aliveCockroaches.Remove(cockroach);
                    if (_aliveCockroaches.Count == 0)
                    {
                        OnAllCockroachesDespawned?.Invoke();
                    }
                }

                cockroach.OnDespawn += OnDespawn;
            }
        }
    }
}