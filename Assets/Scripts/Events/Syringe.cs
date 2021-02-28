using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Syringe : MonoBehaviour
{
    private bool clicked = false;
    private float maxHeight = 0.13f;
    private float maxWidth = 1.1f;
    GameObject syringeBack;
    GameObject syringeMain;
    GameObject syringeTip;
    GameObject mask;
    GameObject bloodMask;
    GameObject bras;
    GameObject background;
    private Vector2 target;
    private Vector2 shake;
    Vector2 diff;
    Vector2 mouseOffset = new Vector2(-1.3f, 0.5f);
   
    FollowMouse fm;
    // Start is called before the first frame update
    void Start()
    {

        if (GameManager.PickedUpSeringue)
        {
            if (!GameManager.PickedUpBlood)
            {
                
                StartEvent();
                
            }
            else
            {
                
                GameObject.Find("UI Text").GetComponent<UIText>().DisplayText("I already stored some blood.", 2f, false);
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>().UnlockMovement();
                Destroy(gameObject);
                
            }

        }
        else
        {
            
            GameObject.Find("UI Text").GetComponent<UIText>().DisplayText("A perfect place to store blood.", 2f, false);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>().UnlockMovement();
            Destroy(gameObject);
            
        }
        
    }

    void StartEvent()
    {
        
        bloodMask = transform.Find("Mask").gameObject;
        syringeBack = transform.Find("SyringeBack").gameObject;
        syringeMain = transform.Find("Syringe").gameObject;
        syringeTip = transform.Find("SyringeTip").gameObject;
        mask = transform.Find("TipMask").gameObject;
        bras = transform.Find("Bras").gameObject;
        background = transform.Find("BG").gameObject;
        fm = GetComponent<FollowMouse>();
        diff = syringeBack.transform.position - syringeMain.transform.position;
        bras.transform.parent = null;
        background.transform.parent = null;
        shake = new Vector2(0, 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        float xMouseOffset = 0;
        float yMouseOffset = 0;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos -= mouseOffset;


        if (Mathf.Abs(mousePos.x - bras.transform.position.x) > maxWidth)
        {
            xMouseOffset = -((mousePos.x - bras.transform.position.x) - Mathf.Sign((mousePos.x - bras.transform.position.x)) * maxWidth);
        }
        if (Mathf.Abs(mousePos.y - bras.transform.position.y) > maxHeight)
        {
            yMouseOffset = -((mousePos.y - bras.transform.position.y) - (Mathf.Sign((mousePos.y - bras.transform.position.y)) * maxHeight));
        }

        
        shake = Vector2.MoveTowards(shake, target, 0.25f*Time.deltaTime);
        fm.xOffset = shake.x + xMouseOffset;
        fm.yOffset = shake.y + yMouseOffset;
        if (Vector2.Distance(shake, target) < 0.01f)
        {
            SetTarget();
        }





        if (Input.GetMouseButtonDown(0) && !clicked)
        {
            clicked = true;
            fm.UnFollow();
            mask.transform.parent = null;
        }
        if (clicked)
        {
            if (Vector2.Distance(mask.transform.position, syringeTip.transform.position) < 0.1f)
            {
                if (Vector2.Distance(syringeBack.transform.position, syringeMain.transform.position) < 1f)
                {
                    bloodMask.transform.localScale = new Vector3((Mathf.Abs((1f - 0.80f*Vector2.Distance(syringeBack.transform.position, syringeMain.transform.position))) / Mathf.Abs((1 - diff.magnitude))), bloodMask.transform.localScale.y, bloodMask.transform.localScale.z);
                    syringeBack.transform.position = Vector2.MoveTowards(syringeBack.transform.position, (Vector2)syringeBack.transform.position+diff, 0.1f * Time.deltaTime);
                }
                else
                {
                    //fin
                    GameObject.FindGameObjectWithTag("CheckMark").GetComponent<Checkmark>().CompletedTask(3);
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>().UnlockMovement();
                    GameManager.PickedUpBlood = true;
                    GameObject.Find("UI Text").GetComponent<UIText>().DisplayText("Human blood goes in the fridge!", 2f);
                    
                    GameManager.CheckWin();
                    
                    Destroy(mask);
                    Destroy(bras);
                    Destroy(background);
                    Destroy(gameObject);
                }
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, mask.transform.position, 0.20f*Time.deltaTime);
            }
        }
        
    }


    void SetTarget()
    {
        target = new Vector2(Random.Range(-0.05f, 0.05f), Random.Range(-0.05f, 0.05f));
    }
}
