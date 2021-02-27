using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    private GameObject light;
    private BoxCollider2D collider;
    // Start is called before the first frame update
    void Start()
    {
        light = transform.Find("Light").gameObject;
        collider = GetComponent<BoxCollider2D>();
    }

 
    public void TurnOff (){

        light.SetActive(false);
        collider.enabled = false;

    }

    public void TurnOn()
    {
        light.SetActive(true);
        collider.enabled = true;
    }

    
}
