using UnityEngine;

public interface IEffect
{
    public void Execute();
}

public class Effect : IEffect
{
    BALLTYPES ballType;
    GameObject gameManagerObject;

    public Effect(Timer timer, float bonusTime, BALLTYPES ballTypes)
    {
        this.timer = timer;
        this.bonusTime = bonusTime;
        this.ballType = ballTypes; 
        gameManagerObject = GameManager.Instance.GetGameObject();
    }

    Timer timer { get; }
    float bonusTime { get; }

    public void Execute() {
        timer.AddTime(bonusTime);
        PlayAudio(); 
    }

    private void PlayAudio()
    {
        AudioClip clip = null;
        Debug.Log("Playing audio type " + ballType.ToString());
        switch (ballType)
        {
            case BALLTYPES.NORMAL_BALL:
                clip = GameManager.Instance.GetAudioNormalHit();
             
                break;
            case BALLTYPES.BOOM:
                clip = GameManager.Instance.GetAudioBoomHit();
            
                break;
            case BALLTYPES.SPECIAL_BALL:
                clip = GameManager.Instance.GetAudioSpecialHit();

                break;
        }
        if (clip
            != null)
        {
            gameManagerObject.GetComponent<AudioSource>().PlayOneShot(clip);
        }
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

