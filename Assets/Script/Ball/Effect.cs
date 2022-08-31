using UnityEngine;

public interface IEffect
{
    public void Execute();
}

public class Effect : IEffect
{
    public Effect(Timer timer, float bonusTime)
    {
        this.timer = timer;
        this.bonusTime = bonusTime;
    }

    Timer timer { get; }
    float bonusTime { get; }

    public void Execute() {
        timer.AddTime(bonusTime);
    }
}

public class ClearingEffect : IEffect
{
    IEffect effect;
    GameManager gameManager;

    // this is a lock to prevent clear calls too deep into stack
    static bool clearing;

    public ClearingEffect(GameManager manager, IEffect effect) {
        this.effect = effect;
        gameManager = manager;
    }

    public void Execute()
    {
        effect.Execute();
        if (!clearing)
        {
            clearing = true;
            gameManager.ClearAllBall();
            clearing = false;
        }
    }
}

