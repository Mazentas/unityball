using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    Timer timer;
    public AudioClip audioNormalHit;
    public AudioClip audioBoomHit;
    public AudioClip audioSpecialHit;
   
    public void Awake()
    {
        timer = Timer.Instance;
        
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public AudioClip GetAudioNormalHit()
    {
        return audioNormalHit;
    }

    public AudioClip GetAudioBoomHit()
    {
        return audioBoomHit;
    }

    public AudioClip GetAudioSpecialHit()
    {
        return audioSpecialHit;
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
