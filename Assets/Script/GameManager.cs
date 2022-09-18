using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class GameManager : Singleton<GameManager>
{
    Timer timer;
   
    bool paused = false;
    public GameObject pausePanel; 

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
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
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

    public void Pause()
    {
        Debug.Log("OPen Pause panel");
        paused = true;
        OpenPausePanel();
    }

    public void Resume()
    {
        paused = false;
        ClosePausePanel();
    }

    public void Quit()
    {
        Application.Quit(); 
    }

    public void OpenPausePanel()
    {
        pausePanel.SetActive(true);
    }

    public void ClosePausePanel()
    {
        GameObject.Find("PausePanel").SetActive(false);
    }

    public void ClearAllBall()
    {
        Debug.Log("Hit all ball");
        foreach (GameObject ball in GameObject.FindGameObjectsWithTag("Ball")) {
            Ball ballScript = ball.GetComponent<Ball>();
            if (ballScript != null)
            {
                if (!ballScript.IsSpecial())
                {
                    ballScript.PlayerDestroy();
                    ballScript.ShowBonusText();
                }
                else
                    Debug.Log("Ignore special ball");
            } else
            {
                Debug.LogError("ball script not found");
            }
        }
    }
}
