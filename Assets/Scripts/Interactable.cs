using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.Rendering.Universal;

[System.Serializable]
public class OnInteract : UnityEvent<GameObject>
{
}

public class Interactable : MonoBehaviour
{

    private bool indicatorEnabled = false;
    public GameObject indicator;
    public Light2D highlight;

    private GameObject ply;

    public OnInteract onInteract;

    private void Start()
    {

        highlight.enabled = false;

    }

    private void Update()
    {

        if (indicator != null)
        {
            
            indicator.SetActive(indicatorEnabled);
            
        }

        if (ply != null)
        {
            
            if ( !ply.GetComponent<PlayerControls>().lockMovement && Input.GetKeyDown(KeyCode.Return))
            {
                
                onInteract.Invoke(ply);
                
            }
            
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag.Equals("Player"))
        {

            ply = other.gameObject;
            indicatorEnabled = true;
            highlight.enabled = true;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.gameObject.tag.Equals("Player"))
        {

            ply = null;
            indicatorEnabled = false;
            highlight.enabled = false;
        }

    }
    
}
