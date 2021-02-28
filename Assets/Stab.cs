using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stab : MonoBehaviour
{
    private bool stabbing = false;
    private FollowMouse fm;
    private void Start()
    {
        fm = GetComponent<FollowMouse>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!stabbing && Input.GetMouseButton(0))
        {
            StartCoroutine(Stabbing());
            stabbing = true;
        }
    }

    IEnumerator Stabbing()
    {   
        Vector3 scale = transform.localScale;
        Vector3 targetScale = 0.5f * scale;  
        

        while(true)
        {
            
            if (transform.localScale.x > targetScale.x)
            {
                transform.localScale -= 1.5f*new Vector3(Time.deltaTime, Time.deltaTime, 0);
                fm.yOffset += 0.2f*Time.deltaTime;
                yield return null;
            }
            else
            {
                break;
            }
            
        }

        while (true)
        {
            if (transform.localScale.x < scale.x)
            {

                transform.localScale += 3f * new Vector3(Time.deltaTime, Time.deltaTime, 0);
                fm.yOffset -= 0.4f * Time.deltaTime;
                yield return null;
            }
            else
            {
                transform.localScale = scale;
                fm.yOffset = 0;

                stabbing = false;
                break;
            }
        }

       
        yield return 0;
      
    }
}
