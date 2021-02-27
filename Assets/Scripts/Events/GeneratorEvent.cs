using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorEvent : MonoBehaviour
{

    public GameObject bar;
    public Transform barTop, barBottom;

    private bool started = false;

    private bool barTaken = false;
    // Start is called before the first frame update
    void Start()
    {
        StartEvent();
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
            
            EndEvent();
            
        }

    }

    public void EndEvent()
    {
        
        //Debug.Log("EVENT GENERATOR DONE!!!");
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>().UnlockMovement();
        
        Destroy(gameObject);
        
    }
}
