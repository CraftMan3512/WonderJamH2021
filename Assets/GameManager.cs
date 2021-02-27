using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static bool LampeDePoche;
    public static float Sanity=100;

    public static void RemoveSanity(float value)
    {
        if (Sanity <= 0)
        {
            Sanity = 0;
        }
        else
        {
            Sanity -= value;
        }
    }

    public static void AddSanity(float value)
    {
        if (Sanity >= 100)
        {
            Sanity = 100;
        }
        else
        {
            Sanity += value;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
