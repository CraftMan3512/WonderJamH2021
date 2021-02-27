using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChicklenEvent : MonoBehaviour
{
    public GameObject chicken1, chicken2, pointille, couteau;
    public AudioClip knifeSFX;

    private void Start()
    {
        StartEvent();
    }

    public void StartEvent()
    {
        
        chicken1.SetActive(true);
        pointille.SetActive(false);
        couteau.SetActive(true);
        chicken2.SetActive(false);
        
    }

    public void OnTakeCouteau()
    {
        
        SoundPlayer.PlaySFX(knifeSFX);
        pointille.SetActive(true);
        
    }

    public void OnCutChicken()
    {
        
        pointille.SetActive(false);
        chicken1.SetActive(false);
        chicken2.SetActive(true);
        
    }

    public void OnTakeHeart()
    {
        
        EndEvent();
        
    }

    void EndEvent()
    {
        
        //Debug.Log("EVENT DONE!!!");
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>().UnlockMovement();
        GameObject.FindGameObjectWithTag("CheckMark").GetComponent<Checkmark>().CompletedTask(1);
        GameObject.Find("UI Text").GetComponent<UIText>().DisplayText("Found a chicken's heart!", 2f);
        Destroy(gameObject);
        
    }
    
}
