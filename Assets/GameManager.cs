using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static bool LampeDePoche;
    public static float Sanity=100;
    public static float Difficulter=1f;
    public static int Encounters = 0;

    public static bool PickedUpOuija = false,
        PickedUpKnife = false,
        PickedUpBlood = false,
        PickedUpFinger = false,
        PickedUpHeart = false,
        PickedUpChicken = false,
        PickedUpSeringue = false,
        PickedUpNote = false;

    public static void RemoveSanity(float value)
    {
        if (Sanity <= 0)
        {
            Sanity = 0;
        }
        else
        {
            Sanity -= value*Difficulter;
        }
    }

    public static void AddSanity(float value)
    {
        if (Sanity >= 100)
        {
            Sanity = 100;
        }
        else
        {
            Sanity += value;
        }
    }

    public static void CheckWin()
    {

        if (PickedUpBlood && PickedUpHeart && PickedUpFinger)
        {
            
            Debug.Log("WIN!!!!!!");
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>().LockMovement();
            SceneChanger.ChangeScene(SceneTypes.WinScene);
            
        }
        
    }

    public static void ResetValues()
    {
        LampeDePoche = false;
        Sanity = 100;
        Difficulter = 1f;
        Encounters = 0;
        
        //story related bools
        PickedUpOuija = false;
        PickedUpKnife = false;
        PickedUpBlood = false;
        PickedUpFinger = false;
        PickedUpHeart = false;
        PickedUpChicken = false;
        PickedUpSeringue = false;
        PickedUpNote = false;
    }

}
