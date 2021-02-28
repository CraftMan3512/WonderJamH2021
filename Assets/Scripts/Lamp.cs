using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    private GameObject light;
    private GameObject light2;
    private BoxCollider2D collider;
    // Start is called before the first frame update
    void Start()
    {
        light = transform.Find("Light").gameObject;
        light2 = transform.Find("pLight").gameObject;
        collider = GetComponent<BoxCollider2D>();
    }

 
    public void TurnOff (){

        light.SetActive(false);
        light2.SetActive(false);
        collider.enabled = false;

    }

    public void TurnOn()
    {
        light.SetActive(true);
        light2.SetActive(true);
        collider.enabled = true;
    }

    
}
