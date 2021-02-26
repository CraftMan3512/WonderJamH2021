using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour
{
    public bool ActiveCollision = false;
    public Collider2D topFloor;

    private void Update()
    {

        GetComponent<PolygonCollider2D>().enabled = ActiveCollision;


    }


    private void OnCollisionExit2D(Collision2D other)
    {

        if (other.gameObject.tag.Equals("Player"))
        {

            ActiveCollision = false;
            topFloor.enabled = true;

        }
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {

        if (other.gameObject.tag.Equals("Player"))
        {

            if (other.gameObject.GetComponent<PlayerControls>().isStairMode)
            {

                ActiveCollision = true;
                topFloor.enabled = false;

            }
            
        }
        
    }
}
