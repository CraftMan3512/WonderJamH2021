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
    public AudioClip knifeSFX;


    private Vector2 target;
    private Vector2 shake;
    private FollowMouse fm;
    private GameObject line;
    private Vector2 fingerEventPos;
    private bool clicked = false;

    private bool ended = false;

    // Start is called before the first frame update
    void Start()
    {

        if (!GameManager.PickedUpFinger)
        {
            
            if (GameManager.PickedUpOuija)
            {

                if (GameManager.PickedUpKnife)
                {
                
                    StartEvent();
                
                }
                else
                {
                
                    GameObject.Find("UI Text").GetComponent<UIText>().DisplayText("I should find a tool to cut with...", 2f, false);
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>().UnlockMovement();
                    Destroy(transform.parent.gameObject);
                
                }

            }
            else
            {
            
                GameObject.Find("UI Text").GetComponent<UIText>().DisplayText("I need a cutting board...", 2f, false);
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>().UnlockMovement();
                Destroy(transform.parent.gameObject);
            
            }
            
        }
        else
        {
            
            GameObject.Find("UI Text").GetComponent<UIText>().DisplayText("I already cut my finger...", 2f, false);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>().UnlockMovement();
            Destroy(transform.parent.gameObject);
            
        }

    }

    void StartEvent()
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

                if (!ended)
                {
                    
                    SoundPlayer.PlaySFX(knifeSFX);
                    ended = true;
                    StopAllCoroutines();
                    StartCoroutine(EndEvent());
                    
                }

            }
        }



    }

    IEnumerator EndEvent()
    {
        
        yield return new WaitForSeconds(2f);
        
        //Blood shit and fade;
        GameObject.FindGameObjectWithTag("CheckMark").GetComponent<Checkmark>().CompletedTask(4);
        GameObject.Find("UI Text").GetComponent<UIText>().DisplayText("Found some human fingers!", 2f);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>().UnlockMovement();
        GameManager.PickedUpFinger = true;
        
        GameManager.CheckWin();
        
        Destroy(transform.parent.gameObject);
        
    }

    


    void SetTarget()
    {
        target = new Vector2(Random.Range(-shakeAmplitude, shakeAmplitude), Random.Range(-shakeAmplitude, shakeAmplitude));
    }

    void Cut()
    {

    }
}
