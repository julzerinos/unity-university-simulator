using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers
{
    public class MainMenu : MonoBehaviour
    {
        public void StartGame()
        {
            Debug.Log("Click");
            SceneManager.LoadScene("StaticLevel");
        }
    }
}
