using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    public GameObject player;
    public Sprite chick;

    public float speed=1f;

    private float direction;
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Sanity < 50)
        {
            GetComponent<SpriteRenderer>().sprite = chick;
        }
        if (!player)
        {
            player=GameObject.Find("Player");
            direction = player.transform.position.x - transform.position.x;
            if (direction > 0)
                direction = 1;
            else 
                direction = -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position=new Vector3(transform.position.x+direction*speed*Time.deltaTime,transform.position.y,transform.position.z);
    }
}
