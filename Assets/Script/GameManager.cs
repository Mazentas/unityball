using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    Timer timer;

    public void Awake()
    {
        timer = Timer.Instance;
    }

    public void OnBallHit(IEffect effect)
    {
        effect.Execute();
    }

    public void OnTimeOut()
    {
        GameOver();
    }

    public void OnBallOutOfRange()
    {
        timer.ReduceTime(3);
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void ClearAllBall()
    {
        Debug.Log("Hit all ball");
        foreach (GameObject ball in GameObject.FindGameObjectsWithTag("Ball")) {
            Ball ballScript = ball.GetComponent<Ball>();
            if (ballScript != null)
            {
                ballScript.PlayerDestroy();
            } else
            {
                Debug.LogError("ball script not found");
            }
        }
    }
}
