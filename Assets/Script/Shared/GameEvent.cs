using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Event")]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> listeners = new List<GameEventListener>();
    public void TriggerEvent()
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventTriggered();
        }
    }
    public void AddListener(GameEventListener listener)
    {
        listeners.Add(listener);
    }
    public void RemoveListener(GameEventListener listener)
    {
        listeners.Remove(listener);
    }
}

public class GameEventWithParam<T> : ScriptableObject
{
    private List<GameEventListnerWithParam<T>> listeners = new List<GameEventListnerWithParam<T>>();
    public void TriggerEvent(T param)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventTriggered(param);
        }
    }
    public void AddListener(GameEventListnerWithParam<T> listener)
    {
        listeners.Add(listener);
    }
    public void RemoveListener(GameEventListnerWithParam<T> listener)
    {
        listeners.Remove(listener);
    }
}
