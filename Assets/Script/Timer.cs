using UnityEngine;
using UnityEngine.UI;

public class Timer : Singleton<Timer>
{
    public float InitialTimeRemaining = 90;
    public GameEvent TimeoutEvent;
    public Text TimerText;
    public float RemainingTime { get { return remainingTime; } }

    float remainingTime;
    static float totalTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        remainingTime = InitialTimeRemaining;
        totalTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        remainingTime -= Time.deltaTime;
        totalTime += Time.deltaTime;

        if (remainingTime <= 0)
        {
            remainingTime = 0;
            TimeoutEvent.TriggerEvent();
        }
        updateTimer();
    }

    private void updateTimer()
    {
        string remainTimeTxt = $"{Mathf.FloorToInt(remainingTime / 60):00}:{Mathf.FloorToInt(remainingTime % 60):00}";
        TimerText.text = remainTimeTxt;
    }

    public void ReduceTime(float t)
    {
        remainingTime -= t;
    }

    public void AddTime(float t)
    {
        remainingTime += t;
    }

    public static string GetTotalTime()
    {
        return $"{Mathf.FloorToInt(totalTime / 60):00}:{Mathf.FloorToInt(totalTime % 60):00}"; ;
    }
}
