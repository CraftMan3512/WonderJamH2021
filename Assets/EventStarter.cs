using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnStartEvent : UnityEvent<GameObject>
{
}

public class EventStarter : MonoBehaviour
{

    public OnStartEvent onStartEvent;

    public void StartEvent(GameObject ply)
    {
        
        onStartEvent.Invoke(ply);    
            
            
    }
    
}
