using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningChicken : MonoBehaviour
{

    public GameObject player;
    public Animator animator;
    private float chickenSpeed = 0.01f;
    private int walkingSide;
    private float animatorSpeed;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        animatorSpeed = animator.speed;
        StartCoroutine(timer());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        flipChicken();
        CheckPlayerDistance();
    }

    public void flipChicken()
    {
        if(walkingSide == 1)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        } else if(walkingSide == -1)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void walk()
    {
        animator.speed = animatorSpeed;
        rb.MovePosition(new Vector2(rb.position.x + (chickenSpeed * walkingSide),0));
        
    }

    public void RunAway()
    {
        animator.speed = animatorSpeed * 1.5f;
        rb.MovePosition(new Vector2(rb.position.x + (chickenSpeed * walkingSide * 1.5f),0));
    }

    public void CheckPlayerDistance()
    {
        if ((Mathf.Abs(player.transform.position.x - transform.position.x) < 2f && Mathf.Abs(player.transform.position.y - transform.position.y) < 0.5f))
        {
            if((player.transform.position.x - transform.position.x <= 0)) {
                
                walkingSide = 1;
            } else
            {
                walkingSide = -1;
            }
            RunAway();
        } else
        {
            walk();
        }
    }

    public IEnumerator timer()
    {

        while (true) {
           if(Random.Range(1,3) == 1)
            {
                walkingSide = 1;
            } else
            {
                walkingSide = -1;
            }
        yield return new WaitForSeconds(Random.Range(0.5f,3f));
        }   
    }
}
