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

    public GameObject eventObject;

    public OnStartEvent onStartEvent;

    public void StartEvent(GameObject player)
    {
        
        player.GetComponent<PlayerControls>().LockMovement();
        
        onStartEvent.Invoke(player);
        if (eventObject != null)
        {

            GameObject newEvent = Instantiate(eventObject, new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y), eventObject.transform.rotation);

        }
        
    }
    

}