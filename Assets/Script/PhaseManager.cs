using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Util.Constants;

public class PhaseManager : Singleton<PhaseManager>
{
    public Timer timer;
    public BallGenerator ballGenerator;
    public float EarlyGrav = -3.3f;
    public float EarlyVel = 0f;
    public BallSpawn[] EarlySpawnRate;
    public float NormalGrav = -9.81f;
    public float NormalVel = 8f;
    public BallSpawn[] NormalSpawnRate;
    public BallSpawn[] EndSpawnRate;
    
    enum Phase { Early, Normal, End }
    Phase phase;
    bool phaseEffectSet = false;
    
    void Awake()
    {
        timer = Timer.Instance;
        ballGenerator = BallGenerator.Instance;

        // early phase effect;
        phase = Phase.Early;
        Physics2D.gravity = new Vector2(0, EarlyGrav);
        ballGenerator.BallMaxInitVel = EarlyVel;
        ballGenerator.SpawnRates = EarlySpawnRate;
        phaseEffectSet = true;

    }

    void Update()
    {
        switch (phase)
        {
            case Phase.Early:
                // Phase effect

                // Phase transition
                if (Timer.GetTotalTime() > 10f)
                {
                    PhaseChange(Phase.Normal);
                }
                break;

            case Phase.Normal:
                // Phase effect
                if (!phaseEffectSet)
                {
                    Physics2D.gravity = new Vector2(0, NormalGrav);
                    ballGenerator.BallMaxInitVel = NormalVel;
                    ballGenerator.SpawnRates = NormalSpawnRate;
                    phaseEffectSet = true;
                }

                // Phase transition
                if (timer.GetRemainingTime() < 10)
                {
                    PhaseChange(Phase.End);
                }
                break;

            case Phase.End:
                if (!phaseEffectSet)
                {
                    ballGenerator.NextSpawnRates = EndSpawnRate;
                    phaseEffectSet = true;
                }
                break;
        }
    }

    void PhaseChange(Phase phase)
    {
        this.phase = phase;
        phaseEffectSet = false;
    }
}
