using UnityEngine;

public class BallFactory : Singleton<BallFactory>
{
    public GameObject Ball;
    public float[] InitRangeX { get; } = { -2.1f, 2.1f };
    public const float INIT_Y = 5.51f;
    
    public void CreateNewBall(float size, float velX, float posX, float posY = INIT_Y)
    {
        Ball newBall = Instantiate(Ball, new Vector3(posX, posY, 0), Quaternion.identity).GetComponent<Ball>();
        newBall.SetSize(size);
        newBall.SetXVelocity(velX);
    }
}
