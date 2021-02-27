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
    private bool droit =true;
    public GameObject lampePoche;
    private Light2D LampeDePocheLight2d;
    public Slider energy;
    public float energyDownRate;
    public float energyThreshHoldFlash=20;
    private float timeLeftFlash;
    public float timeFlash=0;

    // Start is called before the first frame update
    void Start()
    {
        LampeDePocheLight2d = lampePoche.GetComponent<Light2D>();
        rb = GetComponent<Rigidbody2D>();
        baseScale = transform.localScale;
        LampeDePocheLight2d.enabled = false;
        energy.value = 100;
    }
    private void FixedUpdate()
    {
        MovePlayer();

        if (GameManager.LampeDePoche)
        {
            energy.value -= energyDownRate * Time.deltaTime;
            if (energy.value <= 0)
            {
                ToggleLampeDePoche();
            }else if (energy.value<energyThreshHoldFlash)
            {
                Flash(true);
            }
        }
    }

    private void ToggleLampeDePoche()
    {
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

    private void ToggleOnLampeDePoche()
    {
        lampePoche.GetComponent<Light2D>().enabled = true;
    }

    private bool Flash(bool reset)
    {
        if (reset)
        {
            float temp=(100 - energy.value) / 100;
            float often=10f;
            if (timeLeftFlash <= 0&&Random.Range(0,(int)(often-(temp*often/2)))==0)
            {
                timeLeftFlash = 0.10f;
                ToggleOffLampeDePoche();
            }
            else if (timeLeftFlash > 0)
            {
                timeLeftFlash -= Time.deltaTime * temp;
                if (timeLeftFlash <= 0)
                {
                    ToggleOnLampeDePoche();
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
    }

    public void LockMovement()
    {

        lockMovement = true;

    }

    public void UnlockMovement()
    {

        lockMovement = false;

    }
    
    void MovePlayer()
    {

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
        
    }

    private void Interactions()
    {
        if (Input.GetKeyDown("f")&&!Flash(false))
        {
            ToggleLampeDePoche();
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
