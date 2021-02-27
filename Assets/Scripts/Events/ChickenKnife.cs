using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenKnife : MonoBehaviour
{

    public bool leftPressed = false;

    public int nbColliderPressed = 0;

    public GameObject pointille;

    public GameObject chickenEvent;

    // Update is called once per frame
    void Update()
    {

        leftPressed = Input.GetMouseButton(0);
        if (Input.GetMouseButtonUp(0)) nbColliderPressed = 0;

        if (pointille.transform.childCount == nbColliderPressed)
        {
            
            chickenEvent.GetComponent<ChicklenEvent>().OnCutChicken();
            GetComponent<FollowMouse>().UnFollow();
            Destroy(gameObject);
            
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (leftPressed)
        {

            nbColliderPressed++;

        }
        
    }
}
