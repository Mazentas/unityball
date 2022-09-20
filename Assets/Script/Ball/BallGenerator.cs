using System.Collections.Generic;
using UnityEngine;
using static Util.Constants;

[System.Serializable]
public struct BallSpawn
{
    public GameObject ball;
    public int rate;
}

[System.Serializable]
public struct MaxBallType
{
    public BALLTYPES type;
    public uint max;
}

public class BallGenerator : Singleton<BallGenerator>
{
    [HideInInspector]
    public BallSpawn[] SpawnRates; // default spawn rates
    public MaxBallType[] MaxBallTypes;
    public float InitY = 5.51f;
    public float[] InitRangeX = { -2.1f, 2.1f };
    public float[] SpawnCoolDownRange = { 0.2f, 3 };
    public int[] NumberSpawnRange = { 1, 4 };
    public float BallMaxInitVel = MAX_VEL;
    public float BallTTL = 10;
    public int MaxBallsCount = 6;
    int ballCount = 0;

    public static Dictionary<BALLTYPES, uint> TypeCount = new Dictionary<BALLTYPES, uint>();

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

    GameObject GetRandomBallTemplate()
    {
        int sum = 0;
        List<BallSpawn> validSpawnRates = new List<BallSpawn>();

        // Recalculate spawn rate of each type in case some
        // ball types exceed maximum number allowed.
        // New spawn rate is calculate by
        // rate of each ball type divided by
        // sum of all ball types that has yet to exceed maximum amount.
        foreach (BallSpawn spawnrate in NextSpawnRates)
        {
            BALLTYPES type = spawnrate.ball.GetComponent<IBall>().GetBallType();

            uint maxForType = uint.MaxValue;
            foreach (MaxBallType max in MaxBallTypes)
            {
                if (type == max.type)
                {
                    maxForType = max.max;
                    break;
                }
            }

            if (TypeCountOf(type) < maxForType)
            {
                sum += spawnrate.rate;
                validSpawnRates.Add(spawnrate);
            }
        }

        int randn = Random.Range(0, sum);
        for (var i = 0; i < validSpawnRates.Count; i++)
        {
            randn -= validSpawnRates[i].rate;
            if (randn <= 0)
                return validSpawnRates[i].ball;
        }
        return validSpawnRates[validSpawnRates.Count - 1].ball;
    }

    private void CreateNewBall(GameObject ballTemp, float posX, float posY)
    {
        GameObject ballObj = Instantiate(ballTemp, new Vector3(posX, posY, 0), Quaternion.identity);
        IBall newBall = ballObj.GetComponent<IBall>();
        IncBallTypeCount(newBall.GetBallType());
        newBall.SetXVelocity(Random.Range(-BallMaxInitVel, BallMaxInitVel));
    }

    private void NewRandomBall()
    {
        GameObject randBall = GetRandomBallTemplate();
        CreateNewBall(
            randBall,
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

    void DecreaseBallCount()
    {
        ballCount--;
    }

    public void OnBallDestroy(IEffect effect)
    {
        DecBallTypeCount(effect.Type);
        DecreaseBallCount();
    }

    void IncBallTypeCount(BALLTYPES type)
    {
        if (TypeCount.ContainsKey(type))
        {
            TypeCount[type]++;
        } else
        {
            TypeCount.Add(type, 1);
        }
    }

    void DecBallTypeCount(BALLTYPES type)
    {
        if (TypeCount.ContainsKey(type))
        {
            TypeCount[type]--;
            Debug.Log($"type {type} count {TypeCount[type]}");
        } else
        {
            Debug.LogError("Ball types not yet registerd");
        }
    }

    uint TypeCountOf(BALLTYPES type)
    {
        if (TypeCount.ContainsKey(type))
        {
            return TypeCount[type];
        }
        else
            return 0;
    }
}
