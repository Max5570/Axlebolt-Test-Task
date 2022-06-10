using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        public GameObject RestartWindow;
        public Button RestartButton;

        private void Awake()
        {
            RestartButton.onClick.AddListener(HideRestartWindow);
        }

        public void ShowRestartWindow()
        {
            RestartWindow.SetActive(true);
        }
        
        public void HideRestartWindow()
        {
            RestartWindow.SetActive(false);
        }

        private void OnDestroy()
        {
            RestartButton.onClick.RemoveListener(HideRestartWindow);
        }
    }
}