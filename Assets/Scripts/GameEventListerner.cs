using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class CustomGameEvent : UnityEvent<Component, object> { }
public class GameEventListerner : MonoBehaviour 
{
    public GameEvent gameEvent;

    public CustomGameEvent response;

    private void OnEnable()
    {
        if(gameEvent != null)
            gameEvent.RegisterListener(this);
    }

    private void OnDisable()
    {
        if(gameEvent != null) 
            gameEvent.UnRegisterListener(this);
    }
    
    public void OnEventRaise(Component sender, object data)
    {
        response.Invoke(sender, data);
    }
}
