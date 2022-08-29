using UnityEngine;

public class BallGenerator : Singleton<BallGenerator>
{
    public GameObject Ball;
    public  float InitY = 5.51f;
    public float[] BallSizeRange = { 0.5f, 1f };
    public float[] BallInitVel = { 0f, 12f };
    public float[] InitRangeX = { -2.1f, 2.1f };
    public float[] SpawnCoolDownRange = { 0.2f, 3 };
    public int[] NumberSpawnRange = { 1, 4 };
    public float BallTTL = 10;

    float spawnCoolDown = 0;

    public void Update()
    {
        spawnCoolDown -= Time.deltaTime;
        if (spawnCoolDown <= 0)
        {
            SpawnBalls();
            spawnCoolDown = Random.Range(SpawnCoolDownRange[0], SpawnCoolDownRange[1]);
        }
    }

    private void CreateNewBall(Effect effect, float size, float velX, float posX, float posY, float ttl)
    {
        Ball newBall = Instantiate(Ball, new Vector3(posX, posY, 0), Quaternion.identity).GetComponent<Ball>();
        newBall.SetEffect(effect);
        newBall.SetSize(size);
        newBall.SetXVelocity(velX);
        newBall.ttl = 10;
    }

    private void NewRandomBall()
    {
        CreateNewBall(
            new Effect(),
            Random.Range(BallSizeRange[0], BallSizeRange[1]),
            Random.Range(BallInitVel[0], BallInitVel[1]),
            Random.Range(InitRangeX[0], InitRangeX[1]),
            InitY,
            BallTTL);
    }

    private void SpawnBalls()
    {
        for (int i = 0; i < Random.Range(NumberSpawnRange[0], NumberSpawnRange[1]); i++)
        {
            NewRandomBall();
        }
    }
}
