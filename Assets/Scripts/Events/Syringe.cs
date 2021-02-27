using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Syringe : MonoBehaviour
{
    private bool clicked = false;
    GameObject syringeBack;
    GameObject syringeMain;
    GameObject syringeTip;
    GameObject mask;
   
    FollowMouse fm;
    // Start is called before the first frame update
    void Start()
    {
        syringeBack = transform.Find("SyringeBack").gameObject;
        syringeMain = transform.Find("Syringe").gameObject;
        syringeTip = transform.Find("SyringeTip").gameObject;
        mask = transform.Find("TipMask").gameObject;
        fm = GetComponent<FollowMouse>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !clicked)
        {
            clicked = true;
            fm.UnFollow();
            mask.transform.parent = null;
        }
        if (clicked)
        {
            if (Vector2.Distance(mask.transform.position, syringeTip.transform.position) < 0.01f)
            {
                if (Vector2.Distance(syringeBack.transform.position, syringeMain.transform.position) > 0.05f)
                {
                    syringeBack.transform.position = Vector2.MoveTowards(syringeBack.transform.position, syringeMain.transform.position, 0.8f * Time.deltaTime);
                }
                else
                {
                    //fin
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>().UnlockMovement();
                    Destroy(gameObject);
                }
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, mask.transform.position, 0.05f*Time.deltaTime);
            }
        }
        
    }
}
