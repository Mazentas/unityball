using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    Timer timer;
    public AudioClip audioNormalHit;
    public AudioClip audioBoomHit;
    public AudioClip audioSpecialHit;
   
    public bool paused = false;

    public void Awake()
    {
        timer = Timer.Instance;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void PlayAudio(AudioClip audio)
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(audio);
    }

    public void Update()
    {
        if (paused)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
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
                if (!ballScript.IsSpecial())
                    ballScript.PlayerDestroy();
                else
                    Debug.Log("Ignore special ball");
            } else
            {
                Debug.LogError("ball script not found");
            }
        }
    }
}
