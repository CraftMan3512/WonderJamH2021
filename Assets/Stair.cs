using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour
{
    public bool ActiveCollision = false;
    public Collider2D topFloor;
    private bool playerCollided = false;

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
            playerCollided = false;

        }
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag.Equals("Player"))
        {

            if (ActiveCollision)
            {
                
                playerCollided = true;   
                
            }

        }
        
    }

    public void StartStair()
    {
        
        ActiveCollision = true;
        topFloor.enabled = false;
        
    }

    public void CheckEndStairs()
    {

        if (ActiveCollision && !playerCollided)
        {
            
            ActiveCollision = false;
            topFloor.enabled = true;
            playerCollided = false;
            
        }
        
    }
}
