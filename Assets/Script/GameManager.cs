using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public float[] BallSizeRange = { 0.5f, 1f };
    public float[] BallInitVel = { 0f, 12f };
    public float[] SpawnCoolDownRange = { 0.2f, 3 };
    public int[] NumberSpawnRange = { 1, 4 };

    Timer timer;
    BallFactory factory;
    float spawnCoolDown = 0;

    int ballCount = 0;

    public void Awake()
    {
        timer = Timer.Instance;
        factory = BallFactory.Instance;
    }

    public void Start()
    {
    }

    public void Update()
    {
        spawnCoolDown -= Time.deltaTime;
        if (spawnCoolDown <= 0)
        {
            spawnBalls();
        }
    }

    private void newRandomBall()
    {
        factory.CreateNewBall(
            Random.Range(BallSizeRange[0], BallSizeRange[1]),
            Random.Range(BallInitVel[0], BallInitVel[1]),
            Random.Range(factory.InitRangeX[0], factory.InitRangeX[1]));
        spawnCoolDown = Random.Range(SpawnCoolDownRange[0], SpawnCoolDownRange[1]);
        ballCount++;
    }

    private void spawnBalls()
    {
        for (int i = 0; i < Random.Range(NumberSpawnRange[0], NumberSpawnRange[1]); i++)
        {
            newRandomBall();
        }
    }

    public void OnBallHit()
    {
        timer.AddTime(1);
        ballCount--;
    }

    public void OnTimeOut()
    {
        GameOver();
    }

    public void OnBallOutOfRange()
    {
        timer.ReduceTime(3);
        ballCount--;
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
