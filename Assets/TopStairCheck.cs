using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopStairCheck : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {

        if (other.gameObject.tag.Equals("Player"))
        {

            if (Input.GetKey(KeyCode.S))
            {
                
                transform.parent.GetComponent<Stair>().StartStair();

            }
            
        }
        
    }
}
