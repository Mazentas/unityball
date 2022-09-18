using UnityEngine;
using UnityEngine.UI;
using static Util.Constants;

public interface IBall
{
    public void SetTTL(float ttl);
    public void SetSize(float size);
    public void SetXVelocity(float velocX);
    public void SetEffect(IEffect effect);
    public void PlayerDestroy();
    public bool IsSpecial();
}

public enum BALLTYPES
{
    SPECIAL_BALL, NORMAL_BALL, BOOM
}


public class Ball : MonoBehaviour, IBall
{
    public AudioClip ballhitAudio;
    public float BonusTime = InitBonus;
    public float[] BallSizeRange = { 0.1f, 0.2f };
    public float BallMaxInitVel = MaxVel;
    public float ttl = InitTtl;
    public BallHitEvent BallHitEvent;
    public GameEvent BallOutOfRangeEvent;
    public IEffect Effect { get { return effect; } }

    protected IEffect effect;
    public GameObject floatUpParent;
    [SerializeField] protected bool Special = false; // not destroy if special ball hit;

    void Awake()
    {
        gameObject.tag = "Ball";
        this.effect = new Effect(Timer.Instance, BonusTime, ballhitAudio);
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

        if (gameObject.transform.position.x > MaxX || gameObject.transform.position.y > MaxY ||
            gameObject.transform.position.x < MinX || gameObject.transform.position.y < MinY )
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

    public bool IsSpecial()
    {
        return Special;
    }

    void OnMouseDown()
    {
        PlayerDestroy();
        ShowBonusText();

    }

    public void ShowBonusText()
    {
        GameObject floatUp = Instantiate(floatUpParent, transform.position, Quaternion.identity);

        if (BonusTime == BonusBoom)
        {
            floatUp.transform.GetChild(0).GetComponent<TextMesh>().text = BonusBoomText;
            floatUp.transform.GetChild(0).GetComponent<Animator>().Play(FloatUpBoom);
        }
        else
        {

            floatUp.transform.GetChild(0).GetComponent<TextMesh>().text = BonusNormalText;
            floatUp.transform.GetChild(0).GetComponent<Animator>().Play(FloatUpNormal);
        }

    
    }
}
