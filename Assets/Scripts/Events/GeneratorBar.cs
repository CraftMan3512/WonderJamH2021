using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorBar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        
        //Debug.Log("DOWNWW");
        transform.parent.GetComponent<GeneratorEvent>().TakeBar();
        
    }

    private void OnMouseUp()
    {
        //Debug.Log("UUUUUUUUUUUUUUp");
        transform.parent.GetComponent<GeneratorEvent>().ReleaseBar();
        
    }
}
