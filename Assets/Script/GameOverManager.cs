using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }
}
