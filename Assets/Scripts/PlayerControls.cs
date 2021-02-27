using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerControls : MonoBehaviour
{

    public bool lockMovement;
    public float speed;
    private Rigidbody2D rb;
    private Vector3 baseScale;
    private bool droit =true;
    public GameObject lampePoche;
    private Light2D LampeDePocheLight2d;

    // Start is called before the first frame update
    void Start()
    {
        LampeDePocheLight2d = lampePoche.GetComponent<Light2D>();
        rb = GetComponent<Rigidbody2D>();
        baseScale = transform.localScale;
    }
    private void FixedUpdate()
    {
        MovePlayer();
       
        
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
            
            //update movement anim value
            GetComponent<Animator>().SetBool("IsMoving", Input.GetAxisRaw("Horizontal") != 0);

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
        if (Input.GetKeyDown("f"))
        {
            if (GameManager.LampeDePoche) 
            {
                lampePoche.GetComponent<Light2D>().enabled = false;
                GameManager.LampeDePoche = false;
            }
            else
            {
                lampePoche.GetComponent<Light2D>().enabled = true;
                GameManager.LampeDePoche = true;
            }
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
