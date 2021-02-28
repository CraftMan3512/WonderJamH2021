using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorEvent : MonoBehaviour
{

    public GameObject bar;
    public Transform barTop, barBottom;
    public AudioClip leverSFX;

    private bool started = false;

    private bool barTaken = false;
    // Start is called before the first frame update
    void Start()
    {

        if (GameObject.Find("PowerOutage").GetComponent<PowerOutage>().isOutage())
        {
            
            StartEvent();   
            
        }
        else
        {
            
            GameObject.Find("UI Text").GetComponent<UIText>().DisplayText("The breaker is on.", 2f, false);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>().UnlockMovement();
            Destroy(gameObject);
            
        }
    }

    public void StartEvent()
    {
        
        started = true;

    }

    // Update is called once per frame
    void Update()
    {

        if (barTaken && started)
        {
            
            bar.transform.position = new Vector3(bar.transform.position.x, Mathf.Clamp(Camera.main.ScreenToWorldPoint(Input.mousePosition).y, barBottom.position.y, barTop.position.y));
            
        }
        
    }

    public void TakeBar()
    {

        barTaken = true;

    }

    public void ReleaseBar()
    {

        barTaken = false;
        if (Math.Abs(bar.transform.position.y - barTop.position.y) < 0.05f)
        {
            
            SoundPlayer.PlaySFX(leverSFX);
            EndEvent();
            
        }

    }

    public void EndEvent()
    {
        
        GameObject.Find("PowerOutage").GetComponent<PowerOutage>().Fix();
        
        //Debug.Log("EVENT GENERATOR DONE!!!");
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>().UnlockMovement();
        
        Destroy(gameObject);
        
    }
}
