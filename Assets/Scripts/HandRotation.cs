using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandRotation : MonoBehaviour
{

    public float MaxAngle = 90;
    public float angle;
    public float MinAngle = -90;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!transform.parent.GetComponent<PlayerControls>().lockMovement) RotateHand();

    }

    void RotateHand()
    {
        
        Vector2 rel;

        if (transform.parent.GetComponent<PlayerControls>().droit)
        {
            
            rel = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition) - new Vector2(transform.position.x, transform.position.y);
            angle = Mathf.Rad2Deg*Mathf.Atan2(rel.y, rel.x);
            
        }
        else
        {
            rel = new Vector2(transform.position.x, transform.position.y) - (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);
            angle = Mathf.Rad2Deg*Mathf.Atan2(rel.y, rel.x);
            
        }
        
        transform.rotation = Quaternion.Euler(0,0,Mathf.Clamp(angle, MinAngle, MaxAngle));
        
    }

    private void OnDrawGizmos()
    {

        //Gizmos.DrawLine(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));

    }
}
