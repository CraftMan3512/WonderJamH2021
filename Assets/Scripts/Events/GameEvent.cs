using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class GameEvent : MonoBehaviour
{

    public GameObject eventObject;

    public OnStartEvent onStartEvent;

    public void StartEvent(GameObject player)
    {
        
        player.GetComponent<PlayerControls>().LockMovement();
        
        onStartEvent.Invoke(player);
        if (eventObject != null)
        {

            GameObject newEvent = Instantiate(eventObject, new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y), Quaternion.identity);
            newEvent.GetComponent<EventStarter>().StartEvent(player);

        }
        
    }
    

}