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
    private AudioClip stepSound;
    private AudioClip flashlightSFX;
    private AudioClip fastPasSFX;
    private AudioClip[] haaSFX = new AudioClip[2];
    public float timeBeforeCrank=1f;
    public GameObject PrefabFlashLightMonster;
    private GameObject CurrFlashLightMonster;
    public float TimeFlashMonster;
    private float TimeLeftFlashMonster;
    private float TimeLeftNextShadow;
    public GameObject PrefabShadowMonster;

    private GameObject TabText;
    private GameObject ControlsText;
    private bool ControlsOn;

    private bool dead = false;

    // Start is called before the first frame update
    void Start()
    {

        stepSound = Resources.Load<AudioClip>("SFX/SFX_Pas_Personnage_01");
        flashlightSFX = Resources.Load<AudioClip>("SFX/SFX_Click_Lampe_de_Poche");
        fastPasSFX = Resources.Load<AudioClip>("SFX/SFX_fast_pas");
        haaSFX[0] = Resources.Load<AudioClip>("SFX/sfx_haa_01");
        haaSFX[1] = Resources.Load<AudioClip>("SFX/sfx_haa_02");
        
        LampeDePocheLight2d = lampePoche.GetComponent<Light2D>();
        rb = GetComponent<Rigidbody2D>();
        baseScale = transform.localScale;
        LampeDePocheLight2d.enabled = false;
        energy.value = 100;
        TimeLeftNextShadow = Random.Range(0, 10);

        TabText = GameObject.Find("TabText");
        ControlsText=GameObject.Find("ControlsText");
        ControlsText.SetActive(false);
        ControlsOn = false;
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }

   
    private void ToggleLampeDePoche()
    {
        SoundPlayer.PlaySFX(flashlightSFX, 0.25f);
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
                
                SoundPlayer.PlaySFX(haaSFX[Random.Range(0,2)], 4f);
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

    private void ToggleTabControls()
    {
        if (ControlsOn)
        {
            ControlsText.SetActive(false);
            TabText.SetActive(true);
            ControlsOn = false;
        }
        else
        {
            ControlsText.SetActive(true);
            TabText.SetActive(false);
            ControlsOn = true;
        }
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            Debug.Log("Tab");
            ToggleTabControls();
        }
        CheckDeath();
        if (!lockMovement) Interactions();
        
        if (GameManager.LampeDePoche)
        {
            int temp = GetComponent<Sanity>().encounter ? 0 : 1;
            energy.value -= energyDownRate * Time.deltaTime*temp;
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

        if (!CurrFlashLightMonster&&GameManager.Sanity<50&& TimeLeftNextShadow<=0)
        {
            
            SoundPlayer.PlaySFX(fastPasSFX, 1.5f);
            
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

    void CheckDeath()
    {

        if (GameManager.Sanity <= 0 && !dead)
        {
            
            //DEATH
            dead = true;
            LockMovement(); 
            SoundPlayer.PlaySFX(Resources.Load<AudioClip>("SFX/SFX_Death"), 4f);
            GetComponent<SceneChanger>().ChangeScene();
            
        }
        
    }

    public void LockMovement()
    {

        StopAllCoroutines();
        GetComponent<AudioSource>().Stop();
        lockMovement = true;

    }

    public void UnlockMovement()
    {
        
        lockMovement = false;
        //to remove the vanishing cursor bug
        Cursor.visible = true;

    }

    public void PlayStepSound()
    {
        
        GetComponent<AudioSource>().PlayOneShot(stepSound, 10f);
        
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

        if (Input.GetKeyDown(KeyCode.Space))
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
        GetComponent<AudioSource>().Play();
        
        yield return new WaitForSeconds(timeBeforeCrank);
        //play crank sound here
        
        while (Input.GetKey(KeyCode.Space) && energy.value < 100)
        {

            energy.value += (100f / crankTime) * Time.deltaTime;
            yield return null;

        }
        
        GetComponent<AudioSource>().Stop();
        
        //play crank sound end here? or maibe open flashlight
        UnlockMovement();
        //Debug.Log("CRANK FLASH END!");
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Monstre")&&!lockMovement)
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
