using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnStartEvent : UnityEvent<GameObject>
{
}

public class GameEvent : MonoBehaviour
{
    public OnStartEvent onStartEvent;

    public void StartEvent(GameObject player)
    {
        
        player.GetComponent<PlayerControls>().LockMovement();
        
        onStartEvent.Invoke(player);
        
    }
    

}