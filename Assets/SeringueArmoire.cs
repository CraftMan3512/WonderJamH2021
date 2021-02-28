using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeringueArmoire : MonoBehaviour
{
    public Sprite noSeringueSpr;

    public void PickUpSeringue ()
    {

        if (!GameManager.PickedUpSeringue)
        {
            
            GameManager.PickedUpSeringue = true;
            GetComponent<SpriteRenderer>().sprite = noSeringueSpr;
            GameObject.Find("UI Text").GetComponent<UIText>().DisplayText("Found an empty syringe!", 2f);   
            
        }
        else
        {
            
            GameObject.Find("UI Text").GetComponent<UIText>().DisplayText("Nothing important...", 2f, false); 
            
        }

    }
}
