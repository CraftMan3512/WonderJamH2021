using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandKnife : MonoBehaviour
{
    public float shakeAmplitude;
    public float shakeSpeed;
    public float maxHeight;
    public float maxWidth;
    private float baseHeightOffset;


    private Vector2 target;
    private Vector2 shake;
    private FollowMouse fm;
    private GameObject line;
    private Vector2 fingerEventPos;
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
        fingerEventPos = transform.parent.position;
        fingerEventPos += new Vector2(-0.5f, 0.3f);
        shake = new Vector2(0, 0);
        baseHeightOffset = -1;
        SetTarget();
    }

    // Update is called once per frame
    void Update()
    {
        float xMouseOffset = 0;
        float yMouseOffset = 0;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Mathf.Abs(mousePos.x - fingerEventPos.x) > maxWidth)
        {
            xMouseOffset = -((mousePos.x - fingerEventPos.x) - Mathf.Sign((mousePos.x - fingerEventPos.x))* maxWidth);
        }
        if(Mathf.Abs(mousePos.y - fingerEventPos.y) > maxHeight)
        {
            yMouseOffset = -((mousePos.y - fingerEventPos.y) - (Mathf.Sign((mousePos.y - fingerEventPos.y)) * maxHeight));
        }
        
        shake = Vector2.MoveTowards(shake, target, shakeSpeed * Time.deltaTime);
        fm.xOffset = shake.x+xMouseOffset;
        fm.yOffset = shake.y+yMouseOffset + baseHeightOffset;
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
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(line.transform.position.x, transform.position.y), 1f/(xDistance) * Time.deltaTime*2);
                
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
