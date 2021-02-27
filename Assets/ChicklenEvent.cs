using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChicklenEvent : MonoBehaviour
{
    public GameObject chicken1, chicken2, pointille, couteau, ply;
    
    public void StartEvent(GameObject _ply)
    {

        ply = _ply;
        chicken1.SetActive(true);
        pointille.SetActive(false);
        couteau.SetActive(true);
        chicken2.SetActive(false);
        
    }

    public void OnTakeCouteau()
    {
        
        pointille.SetActive(true);
        
    }

    public void OnCutChicken()
    {
        
        pointille.SetActive(false);
        chicken1.SetActive(false);
        chicken2.SetActive(false);
        
    }

    public void OnTakeHeart()
    {
        
        EndEvent();
        
    }

    void EndEvent()
    {
        
        Debug.Log("EVENT DONE!!!");
        ply.GetComponent<PlayerControls>().UnlockMovement();
        Destroy(gameObject);
        
    }
    
}
