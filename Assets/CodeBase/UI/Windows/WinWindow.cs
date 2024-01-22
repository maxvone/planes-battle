using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CodeBase.UI.Windows
{
    public class WinWindow : MonoBehaviour
    {
        [SerializeField] private Button _playAgainButton;

        private void OnEnable() =>
            _playAgainButton.onClick.AddListener(PlayAgain);

        private void OnDisable() =>
            _playAgainButton.onClick.RemoveListener(PlayAgain);

        private void PlayAgain() =>
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}