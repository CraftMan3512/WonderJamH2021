using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEvent : MonoBehaviour
{


    public void PlayEvent(GameObject ply)
    {

        Debug.Log("START EVENT");
        StartCoroutine(EventCoroutine(ply));

    }

    public IEnumerator EventCoroutine(GameObject ply)
    {
        
        yield return new WaitForSeconds(1f);
        
        Debug.Log("END EVENT");
        
        ply.GetComponent<PlayerControls>().UnlockMovement();
        
    }
    
}
