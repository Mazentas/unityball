using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Event when <c>Ball</c> is destroyed with 1 parameters of whether it is destroy by player
/// </summary>
[System.Serializable]
public class OnBallDestroyEvent : UnityEvent<bool>
{
}

public class Ball : MonoBehaviour
{
    public float ttl = 10;
    public GameEvent BallHitEvent;
    public GameEvent BallOutOfRangeEvent;

    public void Update()
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

    void OnMouseDown()
    {
        Destroy(gameObject);
        BallHitEvent.TriggerEvent();
    }
}
