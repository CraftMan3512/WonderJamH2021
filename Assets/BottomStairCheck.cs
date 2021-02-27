using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomStairCheck : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {

        if (other.gameObject.tag.Equals("Player"))
        {

            if (Input.GetKey(KeyCode.W))
            {
                
                transform.parent.GetComponent<Stair>().StartStair();

            }
            
        }
        
    }
}
