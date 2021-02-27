using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorEvent : MonoBehaviour
{

    public GameObject bar;
    public Transform barTop, barBottom;
    private GameObject ply;

    private bool started = false;

    private bool barTaken = false;
    // Start is called before the first frame update
    void Start()
    {
        StartEvent(ply);
    }

    public void StartEvent(GameObject _ply)
    {
        
        ply = _ply;
        started = true;

    }

    // Update is called once per frame
    void Update()
    {

        if (barTaken && started)
        {
            
            bar.transform.position = new Vector3(bar.transform.localPosition.x, Mathf.Clamp(Camera.main.ScreenToWorldPoint(Input.mousePosition).y, barBottom.localPosition.y, barTop.localPosition.y));
            
        }
        
    }

    public void TakeBar()
    {

        barTaken = true;

    }

    public void ReleaseBar()
    {

        barTaken = false;
        if (Math.Abs(bar.transform.localPosition.y - barTop.localPosition.y) < 0.05f)
        {
            
            EndEvent();
            
        }

    }

    public void EndEvent()
    {
        
        Debug.Log("EVENT GENERATOR DONE!!!");
        //ply.GetComponent<PlayerControls>().UnlockMovement();
        
        Destroy(gameObject);
        
    }
}
