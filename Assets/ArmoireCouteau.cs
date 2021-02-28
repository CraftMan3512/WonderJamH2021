using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoireCouteau : MonoBehaviour
{
    
    public Sprite noCouteauSpr;

    public void PickUpKnife ()
    {

        if (!GameManager.PickedUpKnife)
        {
            
            GameManager.PickedUpKnife = true;
            GetComponent<SpriteRenderer>().sprite = noCouteauSpr;
            GameObject.Find("UI Text").GetComponent<UIText>().DisplayText("Found a sharp knife!", 2f);   
            
        }
        else
        {
            
            GameObject.Find("UI Text").GetComponent<UIText>().DisplayText("Nothing important...", 2f, false); 
            
        }

    }
    
}
