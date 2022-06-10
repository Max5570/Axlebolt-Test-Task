using UI;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private UIManager _uiManagerPrefab;
        
        public override void InstallBindings()
        {
            var uiManager = Container.InstantiatePrefabForComponent<UIManager>(_uiManagerPrefab,
                Vector3.zero, Quaternion.identity, null);

            Container.Bind<UIManager>().FromInstance(uiManager).AsSingle().Lazy();
        }
    }
}