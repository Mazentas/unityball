using UnityEngine;
using static Util.Constants;

public interface IBall
{
    public BALLTYPES GetBallType();
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
    public BALLTYPES Type;
    public AudioClip BallhitAudio;
    public float BonusTime = INIT_BONUS;
    public float[] BallSizeRange = { 0.1f, 0.2f };
    public float ttl = INIT_TTL;
    public BallHitEvent BallHitEvent;
    public GameEvent BallOutOfRangeEvent;
    public IEffect Effect { get { return effect; } }

    protected IEffect effect;
    public GameObject floatUpParent;
    [SerializeField] protected bool Special = false; // not destroy if special ball hit;

    void Awake()
    {
        this.effect = new Effect(Timer.Instance, BonusTime, BallhitAudio, Type);
        SetSize(Random.Range(BallSizeRange[0], BallSizeRange[1]));
    }

    void Update()
    {
        ttl -= Time.deltaTime;
        if (ttl <= 0)
        {
            this.GetComponent<CircleCollider2D>().enabled = false;
        }

        if (gameObject.transform.position.x > MAX_X || gameObject.transform.position.y > MAX_Y ||
            gameObject.transform.position.x < MIN_X || gameObject.transform.position.y < MIN_Y )
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
        if (!GameManager.Instance.paused)
        {
            PlayerDestroy();
            ShowBonusText();
        }
    }

    public void ShowBonusText()
    {
        GameObject floatUp = Instantiate(floatUpParent, transform.position, Quaternion.identity);

        if (BonusTime == PENALTY_BOMB)
        {
            floatUp.transform.GetChild(0).GetComponent<TextMesh>().text = PENALTY_BOMB_TEXT;
            floatUp.transform.GetChild(0).GetComponent<Animator>().Play(FLOAT_UP_BOMB);
        }
        else
        {

            floatUp.transform.GetChild(0).GetComponent<TextMesh>().text = BONUS_NORMAL_TEXT;
            floatUp.transform.GetChild(0).GetComponent<Animator>().Play(FLOAT_UP_NORMAL);
        }
    }

    public BALLTYPES GetBallType()
    {
        return Type;
    }
}
