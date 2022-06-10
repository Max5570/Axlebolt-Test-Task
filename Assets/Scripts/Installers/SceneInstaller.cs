using SceneObjects;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private MainCamera _mainCamera;
        [SerializeField] private TargetZone _targetZonePrefab;
        [SerializeField] private Cockroach _cockroachPrefab;
        [SerializeField] private CockroachSpawner _cockroachSpawnerPrefab;
        [SerializeField] private Pointer _pointerPrefab;
        [SerializeField] private GameController _gameControllerPrefab;
        
        [SerializeField] private Vector3 _cockroachSpawnerSpawnPoint;
        [SerializeField] private Vector3 _targetZoneSpawnPoint;

        public override void InstallBindings()
        {
            Container.Bind<MainCamera>().FromInstance(_mainCamera).AsSingle().Lazy();
            
            var pointer = Container.InstantiatePrefabForComponent<Pointer>(_pointerPrefab,
                Vector3.zero, Quaternion.Euler(0,0,-90), null);
            
            Container.Bind<Pointer>().FromInstance(pointer).AsSingle().Lazy();
            
            var targetZone = Container.InstantiatePrefabForComponent<TargetZone>(_targetZonePrefab,
                _targetZoneSpawnPoint, Quaternion.identity, null);
            
            Container.Bind<TargetZone>().FromInstance(targetZone).AsSingle().Lazy();
            
            Container.BindFactory<Vector2, Cockroach, Cockroach.Factory>()
                .FromMonoPoolableMemoryPool(x =>
                {
                    x.WithInitialSize(2).FromComponentInNewPrefab(_cockroachPrefab);
                })
                .Lazy();
            
            
            var spawner = Container.InstantiatePrefabForComponent<CockroachSpawner>(_cockroachSpawnerPrefab,
                _cockroachSpawnerSpawnPoint, Quaternion.identity, null);
            
            Container.Bind<CockroachSpawner>().FromInstance(spawner).AsSingle().Lazy();
            
            Container.InstantiatePrefabForComponent<GameController>(_gameControllerPrefab,
                Vector3.zero, Quaternion.identity, null);
            
        }
    }
}