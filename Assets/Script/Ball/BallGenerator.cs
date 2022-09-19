using UnityEngine;
using static Util.Constants;

[System.Serializable]
public struct BallSpawn
{
    public GameObject ball;
    public int rate;
}

public class BallGenerator : Singleton<BallGenerator>
{
    public BallSpawn[] SpawnRates; // default spawn rates
    public float InitY = 5.51f;
    public float[] InitRangeX = { -2.1f, 2.1f };
    public float[] SpawnCoolDownRange = { 0.2f, 3 };
    public int[] NumberSpawnRange = { 1, 4 };
    public float BallMaxInitVel = MAX_VEL;
    public float BallTTL = 10;
    public int MaxBallsCount = 6;
    public int ballCount = 0;

    [HideInInspector]
    public BallSpawn[] NextSpawnRates; // spawn rate of only next spawn
    float spawnCoolDown = 0;

    public void Awake()
    {
        int sum = 0;
        foreach (BallSpawn spawnrate in SpawnRates)
        {
            sum += spawnrate.rate;
        }

        if (sum != SUM_SPAWN_RATE)
        {
            Debug.LogError("SpawnRates in correct, SpawnRates should have same length as BallTypes and add up to 10000");

        }
    }

    public void Start()
    {
        NextSpawnRates = SpawnRates;
    }

    public void Update()
    {
        spawnCoolDown -= Time.deltaTime;
        if (spawnCoolDown <= 0)
        {
            SpawnBalls();
            NextSpawnRates = SpawnRates;
            spawnCoolDown = Random.Range(SpawnCoolDownRange[0], SpawnCoolDownRange[1]);
        }
    }

    private int GetRandomBallIndex()
    {
        int randn = Random.Range(0, SUM_SPAWN_RATE);
        for (int i = 0; i < NextSpawnRates.Length; i++)
        {
            randn -= NextSpawnRates[i].rate;
            if (randn <= 0)
                return i;
        }
        return SpawnRates.Length - 1;
    }

    private void CreateNewBall(int index, float posX, float posY)
    {
        GameObject ballObj = Instantiate(NextSpawnRates[index].ball, new Vector3(posX, posY, 0), Quaternion.identity);
        IBall newBall = ballObj.GetComponent<IBall>();
        newBall.SetXVelocity(Random.Range(-BallMaxInitVel, BallMaxInitVel));
    }

    private void NewRandomBall()
    {
        int randi = GetRandomBallIndex();
        CreateNewBall(
            randi,
            Random.Range(InitRangeX[0], InitRangeX[1]),
            InitY);
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
