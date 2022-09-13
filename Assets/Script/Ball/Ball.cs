using UnityEngine;
using UnityEngine.UI;

public interface IBall
{
    public void SetTTL(float ttl);
    public void SetSize(float size);
    public void SetXVelocity(float velocX);
    public void SetEffect(IEffect effect);
    public void PlayerDestroy();
}

public enum BALLTYPES
{
    SPECIAL_BALL, NORMAL_BALL, BOOM
}


public class Ball : MonoBehaviour, IBall
{
    public BALLTYPES balltypes;
    public float BonusTime = 10;
    public float[] BallSizeRange = { 0.1f, 0.2f };
    public float BallMaxInitVel = 8;
    public float ttl = 10;
    public BallHitEvent BallHitEvent;
    public GameEvent BallOutOfRangeEvent;
    public IEffect Effect { get { return effect; } }
    protected IEffect effect;
    public GameObject bonusText; 

    void Awake()
    {
        gameObject.tag = "Ball";
        this.effect = new Effect(Timer.Instance, BonusTime, balltypes);
        SetSize(Random.Range(BallSizeRange[0], BallSizeRange[1]));
        SetXVelocity(Random.Range(-BallMaxInitVel, BallMaxInitVel));
  
    }

    void Update()
    {
        ttl -= Time.deltaTime;
        if (ttl <= 0)
        {
            this.GetComponent<CircleCollider2D>().enabled = false;
        }

        if (gameObject.transform.position.x > 10 || gameObject.transform.position.y > 10 ||
            gameObject.transform.position.x < -10 || gameObject.transform.position.y < -10)
        {
            Destroy(gameObject);
            BallOutOfRangeEvent.TriggerEvent();
        }
    }

    public void SetSize(float size)
    {
        this.GetComponent<Transform>().localScale = new Vector3(size, size, size);
    }

    public void SetXVelocity(float velocX)
    {
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(velocX, 0);
    }

    public void SetEffect(IEffect effect)
    {
        this.effect = effect;
    }

    public void PlayerDestroy()
    {
        Destroy(gameObject);
        BallHitEvent.TriggerEvent(this.effect);

    }

    public void SetTTL(float ttl)
    {
        this.ttl = ttl;
    }

    void OnMouseDown()
    {
        PlayerDestroy();
        showBonusText();

    }

    void showBonusText()
    {
        Debug.Log("Show bonus text");
        GameObject text = Instantiate(bonusText, transform.position, Quaternion.identity);
        text.GetComponent<Text>().text = "+5S";
    }
}
