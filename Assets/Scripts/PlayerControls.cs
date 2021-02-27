using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerControls : MonoBehaviour
{

    public bool lockMovement;
    public float speed;
    private Rigidbody2D rb;
    private Vector3 baseScale;
    public bool droit =true;
    public GameObject lampePoche;
    private Light2D LampeDePocheLight2d;
    public Slider energy;
    public float energyDownRate;
    public float energyThreshHoldFlash=20;
    private float timeLeftFlash;
    public float timeFlash=0;
    public float crankTime = 4f;
    public AudioClip stepSound;
    public AudioClip flashlightSFX;
    public float timeBeforeCrank=1f;
    public GameObject PrefabFlashLightMonster;
    private GameObject CurrFlashLightMonster;
    public float TimeFlashMonster;
    private float TimeLeftFlashMonster;
    private float TimeLeftNextShadow;
    public GameObject PrefabShadowMonster;
    private float timeLastFight;


    // Start is called before the first frame update
    void Start()
    {
        LampeDePocheLight2d = lampePoche.GetComponent<Light2D>();
        rb = GetComponent<Rigidbody2D>();
        baseScale = transform.localScale;
        LampeDePocheLight2d.enabled = false;
        energy.value = 35;
        TimeLeftNextShadow = Random.Range(0, 10);
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }

   
    private void ToggleLampeDePoche()
    {
        SoundPlayer.PlaySFX(flashlightSFX, 0.5f);
        timeLeftFlash = 0;
        if (GameManager.LampeDePoche) 
        {
            lampePoche.GetComponent<Light2D>().enabled = false;
            GameManager.LampeDePoche = false;
        }
        else if(energy.value>0)
        {
            lampePoche.GetComponent<Light2D>().enabled = true;
            GameManager.LampeDePoche = true;
        }
    }

    private void ToggleOffLampeDePoche()
    {
        lampePoche.GetComponent<Light2D>().enabled = false;
    }

    private void ToggleOnLampeDePocheVis()
    {
        lampePoche.GetComponent<Light2D>().enabled = true;
    }

    private bool Flash(bool reset)
    {
        if (reset)
        {
            if (!CurrFlashLightMonster&&GameManager.Sanity<80&&TimeLeftFlashMonster<=0)
            {
                CurrFlashLightMonster = Instantiate(PrefabFlashLightMonster, transform.Find("SpawnFront").position,Quaternion.identity);
                if (transform.localScale.x > 0)
                {
                    CurrFlashLightMonster.transform.localScale = new Vector3(
                        -CurrFlashLightMonster.transform.localScale.x, CurrFlashLightMonster.transform.localScale.y,
                        CurrFlashLightMonster.transform.localScale.z);
                    
                }

                TimeLeftFlashMonster = TimeFlashMonster;
            }else if (TimeLeftFlashMonster > 0)
            {
                TimeLeftFlashMonster -= Time.deltaTime;
            }

            float temp=(100 - energy.value) / 100;
            float often=18f;
            if (timeLeftFlash <= 0&&Random.Range(0,(int)(often-(temp*often/2)))==0)
            {
                timeLeftFlash =timeFlash;
                ToggleOffLampeDePoche();
            }
            else if (timeLeftFlash > 0)
            {
                timeLeftFlash -= Time.deltaTime * temp;
                if (timeLeftFlash <= 0)
                {
                    ToggleOnLampeDePocheVis();
                    timeLeftFlash = 0;
                }

                return true;
            }
        }else if (timeLeftFlash > 0)
            return true;

        return false;

    }
    private void Update()
    {
        
        Interactions();
        
        if (GameManager.LampeDePoche)
        {
            energy.value -= energyDownRate * Time.deltaTime;
            if (energy.value <= 0)
            {
                ToggleLampeDePoche();
            }else if (energy.value < energyThreshHoldFlash)
            {
                if (energy.value <= 0)
                {
                    timeLeftFlash = 0;
                }

                Flash(true);
            }
        }

        if (GameManager.Sanity <= 0&&timeLastFight<=0)
        {
            Instantiate((GameObject)Resources.Load("Events/DemonFight"),new Vector3(Camera.main.transform.position.x,Camera.main.transform.position.y,0),Quaternion.identity);
            LockMovement();
            timeLastFight = 30;
        }else if (timeLastFight > 0)
        {
            timeLastFight -= Time.deltaTime;
        }
        

        if (!CurrFlashLightMonster&&GameManager.Sanity<50&& TimeLeftNextShadow<=0)
        {
            CurrFlashLightMonster = Instantiate(PrefabShadowMonster, transform.Find("SpawnBehind").position,Quaternion.identity);
            if (transform.localScale.x <= 0)
            {
                CurrFlashLightMonster.transform.localScale = new Vector3(
                    -CurrFlashLightMonster.transform.localScale.x, CurrFlashLightMonster.transform.localScale.y,
                    CurrFlashLightMonster.transform.localScale.z);

            }
            TimeLeftNextShadow=Random.Range(10, 20);
            
        }else if (TimeLeftNextShadow > 0&&!CurrFlashLightMonster)
        {
            TimeLeftNextShadow -= Time.deltaTime;
        }
    }

    public void LockMovement()
    {

        lockMovement = true;

    }

    public void UnlockMovement()
    {

        lockMovement = false;

    }

    public void PlayStepSound()
    {
        
        GetComponent<AudioSource>().PlayOneShot(stepSound);
        
    }
    
    void MovePlayer()
    {

        //update movement anim value
        GetComponent<Animator>().SetBool("IsMoving", Input.GetAxisRaw("Horizontal") != 0 && !lockMovement);
        
        rb.velocity = Vector2.zero; // fix 

        if (!lockMovement)
        {
            
            List<RaycastHit2D> hit = new List<RaycastHit2D>();
            int nb = Physics2D.Raycast(transform.position + new Vector3(0,0.2f), Vector3.down, new ContactFilter2D(),hit, 1);

            Vector3 surfaceDir = new Vector3(1,0);
        
            foreach (RaycastHit2D rh in hit)
            {

                if (rh.collider.gameObject.tag.Equals("Stair") && rh.collider.gameObject.GetComponent<Stair>().ActiveCollision)
                {
                
                    surfaceDir = nb == 0 ? new Vector3(0,1) : (Vector3)rh.normal;
                    surfaceDir = -Vector2.Perpendicular(surfaceDir);
                    break;

                }
            
            }

            if (Input.GetAxisRaw("Horizontal") < 0&&droit)
            {
                droit = false;
                transform.localScale = new Vector3(-baseScale.x,baseScale.y,baseScale.z);
            }else if (Input.GetAxisRaw("Horizontal") > 0&&!droit)
            {
                droit = true;
                transform.localScale = new Vector3(baseScale.x,baseScale.y,baseScale.z);
            }
            
            if (Input.GetAxisRaw("Horizontal") == 0) rb.gravityScale = 0;
            else rb.gravityScale = 1;
            
            rb.MovePosition(rb.transform.position + (surfaceDir * (Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime)));    
            
        }
        else
        {

            rb.gravityScale = 0;

        }
        
    }

    private void Interactions()
    {
        if (!lockMovement && Input.GetKeyDown(KeyCode.F))
        {
            ToggleLampeDePoche();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            
            lampePoche.GetComponent<Light2D>().enabled = false;
            GameManager.LampeDePoche = false;
            
            LockMovement();
            StopAllCoroutines();
            StartCoroutine(CrankFlashlight());
        }
    }

    IEnumerator CrankFlashlight()
    {
        yield return new WaitForSeconds(timeBeforeCrank);
        //play crank sound here
        
        while (Input.GetKey(KeyCode.E) && energy.value < 100)
        {

            energy.value += (100f / crankTime) * Time.deltaTime;
            yield return null;

        }
        
        //play crank sound end here? or maibe open flashlight
        UnlockMovement();
        //Debug.Log("CRANK FLASH END!");
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Monstre"))
        {
            Instantiate((GameObject)Resources.Load("Events/DemonFight"),new Vector3(Camera.main.transform.position.x,Camera.main.transform.position.y,0),Quaternion.identity);
            Destroy(other.gameObject);
            LockMovement();
        }
    }

    private void OnDrawGizmos() //to remove later
    {
        
        List<RaycastHit2D> hit = new List<RaycastHit2D>();
        int nb = Physics2D.Raycast(transform.position + new Vector3(0,0.2f), Vector3.down, new ContactFilter2D(),hit, 1);

        Vector3 surfaceDir = new Vector3(1,0);
        
        foreach (RaycastHit2D rh in hit)
        {

            if (rh.collider.gameObject.tag.Equals("Stair") && rh.collider.gameObject.GetComponent<Stair>().ActiveCollision)
            {
                
                surfaceDir = nb == 0 ? new Vector3(0,1) : (Vector3)rh.normal;
                surfaceDir = -Vector2.Perpendicular(surfaceDir);
                break;

            }
            
        }
        Gizmos.DrawLine(transform.position + new Vector3(0, 0.2f), transform.position + new Vector3(0, 0.2f) + surfaceDir.normalized);
        
    }
}
