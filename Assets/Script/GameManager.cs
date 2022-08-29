using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    Timer timer;

    public void Awake()
    {
        timer = Timer.Instance;
    }

    public void OnBallHit(Effect effect)
    {
        timer.AddTime(effect.bonusTime);
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

    public void HitAllBall()
    {
        foreach (GameObject ball in GameObject.FindGameObjectsWithTag("ball")) {
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
