using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sanity : MonoBehaviour
{
    public float sanityMinusMulti=1f;
    public float sanityAddMulti = 1f;
    public bool isInLight=false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isInLight)
        {
            GameManager.AddSanity(Time.deltaTime * sanityAddMulti* GameManager.Difficulter);
        }
        else if (!GameManager.LampeDePoche)
        {
            GameManager.RemoveSanity(Time.deltaTime * sanityMinusMulti);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Light"))
            isInLight = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Light"))
            isInLight = false;
    }
}
