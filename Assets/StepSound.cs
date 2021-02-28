using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepSound : MonoBehaviour
{
    // Start is called before the first frame update
    public void PlayStepSound()
    {
        
        GetComponent<AudioSource>().PlayOneShot(stepSound);
        
    }
}
