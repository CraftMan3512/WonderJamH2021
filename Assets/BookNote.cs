using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookNote : MonoBehaviour
{
    
    public void PickUpNote ()
    {

        if (!GameManager.PickedUpNote)
        {
            
            GameManager.PickedUpNote = true;
            GameObject.Find("UI Text").GetComponent<UIText>().DisplayText("Found instructions to kill the demon!", 2f);   
            Destroy(gameObject);
            
        }
        else
        {
            
            GameObject.Find("UI Text").GetComponent<UIText>().DisplayText("Nothing important...", 2f, false); 
            
        }

    }
    
}
