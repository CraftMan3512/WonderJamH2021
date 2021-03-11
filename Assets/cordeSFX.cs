using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cordeSFX : MonoBehaviour
{

    public void PlayCorde()
    {
        
        GetComponent<AudioSource>().Play();
        
    }

    private void Start()
    {
        Cursor.visible = true;
    }

}
