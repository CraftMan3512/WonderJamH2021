using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBlender : MonoBehaviour
{

    public AudioSource source1, source2;
    public float transitionTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
        //only play source 1 at start
        source2.volume = 0;
        source1.Play();
        source2.Play();
        StartCoroutine(TestCor());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator TestCor()
    {
        
        //Debug.Log("TEST START");
        yield return new WaitForSeconds(5f);
        HastenMusic();
        yield return new WaitForSeconds(10f);
        RelaxMusic();
        yield return new WaitForSeconds(5f);
        //Debug.Log("TEST END");
        
    }

    public void HastenMusic()
    {

        //StopAllCoroutines();
        StartCoroutine(HastenMusicCoroutine());

    }

    IEnumerator HastenMusicCoroutine()
    {

        while (source1.volume > 0 && source2.volume < 1)
        {

            float delta = transitionTime * Time.deltaTime;
            source1.volume = Mathf.Max(0, source1.volume - delta);
            source2.volume = Mathf.Min(1, source2.volume + delta);
            yield return null; // wait for next frame
            
        }

    }

    public void RelaxMusic()
    {

        //StopAllCoroutines();
        StartCoroutine(RelaxMusicCoroutine());

    }

    IEnumerator RelaxMusicCoroutine()
    {
        
        while (source2.volume > 0 && source1.volume < 1)
        {

            float delta = transitionTime * Time.deltaTime;
            source2.volume = Mathf.Max(0, source2.volume - delta);
            source1.volume = Mathf.Min(1, source1.volume + delta);
            yield return null; // wait for next frame
            
        }
        
    }
    
}
