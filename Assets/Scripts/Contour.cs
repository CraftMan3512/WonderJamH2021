using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contour : MonoBehaviour
{

    CanvasGroup cg;
    // Start is called before the first frame update
    void Start()
    {
        cg = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Sanity < 70)
        {
            cg.alpha = 1 - GameManager.Sanity / 100f;
        }
        else
        {
            cg.alpha = 0;
        }
    }
}
