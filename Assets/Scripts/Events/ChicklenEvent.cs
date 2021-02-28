using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChicklenEvent : MonoBehaviour
{
    public GameObject chicken1, chicken2, pointille, couteau, heart;
    public AudioClip knifeSFX;

    private void Start()
    {

        if (!GameManager.PickedUpHeart)
        {
            
            if (GameManager.PickedUpChicken)
            {

                if (GameManager.PickedUpKnife)
                {
                
                    StartEvent();   
                
                }
                else
                {
                
                    GameObject.Find("UI Text").GetComponent<UIText>().DisplayText("I should find a tool to cut with...", 2f, false);
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>().UnlockMovement();
                    Destroy(gameObject);
                
                }

            }
            else
            {
            
                GameObject.Find("UI Text").GetComponent<UIText>().DisplayText("Maybe I should find something to cut before...", 2f, false);
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>().UnlockMovement();
                Destroy(gameObject);
            
            }   
            
        }
        else
        {
            
            GameObject.Find("UI Text").GetComponent<UIText>().DisplayText("I already have his heart.", 2f, false);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>().UnlockMovement();
            Destroy(gameObject);
            
        }

    }

    public void StartEvent()
    {
        
        chicken1.SetActive(true);
        pointille.SetActive(false);
        couteau.SetActive(true);
        chicken2.SetActive(false);
        heart.SetActive(false);
        
        couteau.GetComponent<FollowMouse>().Follow();
        OnTakeCouteau();
        
        
    }

    public void OnTakeCouteau()
    {
        
        SoundPlayer.PlaySFX(knifeSFX);
        pointille.SetActive(true);
        
    }

    public void OnCutChicken()
    {
        
        SoundPlayer.PlaySFX(knifeSFX);
        pointille.SetActive(false);
        chicken1.SetActive(false);
        chicken2.SetActive(true);
        heart.SetActive(true);
        
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
        GameManager.PickedUpHeart = true;
        GameManager.Difficulter += 0.2f;
        
        GameManager.CheckWin();
        
        Destroy(gameObject);
        
    }
    
}
