using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterX : MonoBehaviour
{
    public float temp;

    private float curr;
    // Start is called before the first frame update
    public void SetDurr(float durr)
    {
        temp = durr;
    }

    private void Start()
    {
        if (temp == 0)
        {
            temp = 5;
        }
    }

    // Update is called once per frame
    void Update()
    {
        curr += Time.deltaTime;
        if (curr >= temp)
        {
            Destroy(gameObject);
        }
    }
}
