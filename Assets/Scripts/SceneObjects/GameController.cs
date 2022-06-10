using UI;
using Zenject;
using UnityEngine;

namespace SceneObjects
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private int _cockroachesCountToSpawn = 1;
        
        private CockroachSpawner _cockroachSpawner;
        private UIManager _uiManager;
        private Pointer _pointer;
        
        [Inject]
        private void Construct(CockroachSpawner cockroachSpawner, UIManager uiManager, Pointer pointer)
        {
            _cockroachSpawner = cockroachSpawner;
            _uiManager = uiManager;
            _pointer = pointer;
        }

        private void Awake()
        {
            _cockroachSpawner.OnAllCockroachesDespawned += ShowRestartWindow;
            _uiManager.RestartButton.onClick.AddListener(StartGame);
            StartGame();
        }

        private void StartGame()
        {
            _cockroachSpawner.Spawn(_cockroachesCountToSpawn);
            Cursor.visible = false;
            _pointer.gameObject.SetActive(true);
        }

        public void ShowRestartWindow()
        {
            _uiManager.ShowRestartWindow();
            Cursor.visible = true;
            _pointer.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _cockroachSpawner.OnAllCockroachesDespawned -= ShowRestartWindow;
            _uiManager.RestartButton.onClick.RemoveListener(StartGame);
        }
    }
}