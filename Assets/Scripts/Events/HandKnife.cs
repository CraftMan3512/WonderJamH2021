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
    // Start is called before the first frame update
    void Start()
    {
        if(shakeAmplitude == 0)
        {
            shakeAmplitude = 1;
        }
        if(shakeSpeed == 0)
        {
            shakeSpeed = 1;
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
        if(Vector2.Distance(shake,target) < 0.1f)
        {
            SetTarget();
        }



    }


    void SetTarget()
    {
        target = new Vector2(Random.Range(-shakeAmplitude, shakeAmplitude), Random.Range(-shakeAmplitude, shakeAmplitude));
    }
}
