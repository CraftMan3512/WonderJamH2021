using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trumpet : MonoBehaviour
{
    public Sprite sprite;
    // Start is called before the first frame update
    void Start()
    {
        if(Random.Range(0f,100f) < 15f) {

            GetComponent<SpriteRenderer>().sprite = sprite;
        }
    }

}
