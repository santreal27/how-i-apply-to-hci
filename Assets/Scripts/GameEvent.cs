using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent")]
public class GameEvent : ScriptableObject
{
    public List<GameEventListerner> listeners = new List<GameEventListerner>();

    public void Raise(Component sender, object data)
    {
        for (int i = 0; i < listeners.Count; i++)
        {
            listeners[i].OnEventRaise(sender,data);
        }
    }

    public void RegisterListener(GameEventListerner listener)
    {
        if(!listeners.Contains(listener))
            listeners.Add(listener);

    }

    public void UnRegisterListener(GameEventListerner listener)
    {
        if(listeners.Contains(listener))
            listeners.Remove(listener);
    }
}
