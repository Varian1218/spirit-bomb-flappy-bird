using UnityEngine;
using UnityEngine.SceneManagement;

namespace Widgets
{
    public class GameOverWidget : MonoBehaviour
    {
        public bool Visible
        {
            set => gameObject.SetActive(value);
        }

        public void OnButtonClicked()
        {
            SceneManager.LoadScene("Gameplay");
        }
    }
}