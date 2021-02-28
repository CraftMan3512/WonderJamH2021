using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuijaArmoire : MonoBehaviour
{

    public Sprite noOuijaSpr;

    public void PickUpOuija ()
    {

        if (!GameManager.PickedUpOuija)
        {
            
            GameManager.PickedUpOuija = true;
            GetComponent<SpriteRenderer>().sprite = noOuijaSpr;
            GameObject.FindGameObjectWithTag("CheckMark").GetComponent<Checkmark>().CompletedTask(2);
            GameObject.Find("UI Text").GetComponent<UIText>().DisplayText("Found a ouija board!", 2f);   
            
        }
        else
        {
            
            GameObject.Find("UI Text").GetComponent<UIText>().DisplayText("Nothing important...", 2f, false); 
            
        }

    }
    
}
