using UnityEngine;
using UnityEngine.Events;

public class GameEventListnerWithParam<T> : MonoBehaviour
{
    public GameEventWithParam<T> gameEvent;
    public UnityEvent<T> onEventTriggered;

    void OnEnable()
    {
        gameEvent.AddListener(this);
    }
    void OnDisable()
    {
        gameEvent.RemoveListener(this);
    }

    public void OnEventTriggered(T param)
    {
        onEventTriggered.Invoke(param);
    }
}