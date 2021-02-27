using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandKnife : MonoBehaviour
{
    public float shakeAmplitude;
    public float shakeSpeed;
    private Vector2 target;
    private Vector2 shake;
    private FollowMouse fm;
    private GameObject line;
    private bool clicked = false;
    // Start is called before the first frame update
    void Start()
    {
        if(shakeAmplitude == 0)
        {
            shakeAmplitude = 0.2f;
        }
        if(shakeSpeed == 0)
        {
            shakeSpeed = 0.3f;
        }
        fm = GetComponent<FollowMouse>();
        shake = new Vector2(0, 0);
        SetTarget();
    }

    // Update is called once per frame
    void Update()
    {

        shake = Vector2.MoveTowards(shake, target, shakeSpeed * Time.deltaTime);
        fm.xOffset = shake.x;
        fm.yOffset = shake.y;
        if(Vector2.Distance(shake,target) < 0.05f)
        {
            SetTarget();
        }
        if (Input.GetMouseButtonDown(0) && !clicked)
        {
            clicked = true;
            fm.UnFollow();
            line = transform.GetChild(0).gameObject;
            transform.GetChild(0).SetParent(transform.parent);
     
        }

        if (clicked)
        {
            float xDistance = Mathf.Abs(transform.position.x - line.transform.position.x);
            if (xDistance > 0.05f)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(line.transform.position.x, transform.position.y), 1f/(xDistance) * Time.deltaTime);
                
            }
            else
            {
                //Blood shit and fade;
            }
        }



    }

    


    void SetTarget()
    {
        target = new Vector2(Random.Range(-shakeAmplitude, shakeAmplitude), Random.Range(-shakeAmplitude, shakeAmplitude));
    }

    void Cut()
    {

    }
}
