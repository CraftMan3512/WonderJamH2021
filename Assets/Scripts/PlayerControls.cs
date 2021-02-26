using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    public bool lockMovement;
    public float speed;
    private Rigidbody2D rb;

    public bool isStairMode = false;
    
    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {

        CheckStairMode();

    }

    void CheckStairMode()
    {

        isStairMode = Input.GetKey(KeyCode.W);

    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
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
        
        
        rb.MovePosition(rb.transform.position + (Input.GetAxisRaw("Horizontal")*surfaceDir*speed*Time.deltaTime));

    }

    private void OnDrawGizmos()
    {
        
        List<RaycastHit2D> hit = new List<RaycastHit2D>();
        int nb = Physics2D.Raycast(transform.position + new Vector3(0,0.2f), Vector3.down, new ContactFilter2D(),hit);

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
