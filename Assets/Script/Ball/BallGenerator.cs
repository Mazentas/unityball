using UnityEngine;

public class BallGenerator : Singleton<BallGenerator>
{
    public GameObject[] Balls;
    public int[] SpawnRates;
    public float InitY = 5.51f;
    public float[] InitRangeX = { -2.1f, 2.1f };
    public float[] SpawnCoolDownRange = { 0.2f, 3 };
    public int[] NumberSpawnRange = { 1, 4 };
    public float BallTTL = 10;
    public int MaxBallsCount = 6;

    float spawnCoolDown = 0;
    public int ballCount = 0;

    public void Awake()
    {
        int sum = 0;
        foreach (int rate in SpawnRates)
        {
            sum += rate;
        }

        if (sum != 10000 || Balls.Length != SpawnRates.Length)
        {
            Debug.LogError("SpawnRates in correct, SpawnRates should have same length as BallTypes and add up to 10000");

            // Auto correct rate to equally likely if wrongly configured
            int equalRate = 10000 / Balls.Length;
            SpawnRates = new int[Balls.Length];
            for (int i = 0; i < Balls.Length - 1; i++)
            {   
                SpawnRates[i] = equalRate;
            }
            SpawnRates[Balls.Length - 1] = 10000 - equalRate * (Balls.Length - 1);
        }
    }

    public void Update()
    {
        spawnCoolDown -= Time.deltaTime;
        if (spawnCoolDown <= 0)
        {
            SpawnBalls();
            spawnCoolDown = Random.Range(SpawnCoolDownRange[0], SpawnCoolDownRange[1]);
        }
    }

    private int GetRandomBallIndex()
    {
        int randn = Random.Range(0, 10000);
        for (int i = 0; i < SpawnRates.Length; i++)
        {
            randn -= SpawnRates[i];
            if (randn <= 0)
                return i;
        }
        return SpawnRates.Length - 1;
    }

    private void CreateNewBall(int index, float posX, float posY, float ttl)
    {
        GameObject ballObj = Instantiate(Balls[index], new Vector3(posX, posY, 0), Quaternion.identity);
        IBall newBall = ballObj.GetComponent<IBall>();
        newBall.SetTTL(ttl);
    }

    private void NewRandomBall()
    {
        int randi = GetRandomBallIndex();
        CreateNewBall(
            randi,
            Random.Range(InitRangeX[0], InitRangeX[1]),
            InitY,
            BallTTL);
        ballCount++;
    }

    private void SpawnBalls()
    {
        for (int i = 0; i < Random.Range(NumberSpawnRange[0], NumberSpawnRange[1]); i++)
        {
            if (ballCount < MaxBallsCount)
            {
                NewRandomBall();
            }
        }
    }

    public void DecreaseBallCount()
    {
        ballCount--;
    }
}
