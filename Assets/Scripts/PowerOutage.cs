using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerOutage : MonoBehaviour
{

    private float timeUntilNextPowerOutage;
    private bool outage = false;
    GameObject[] lamps;

    private bool firstOutage = true;
    
    // Start is called before the first frame update
    void Start()
    {
        
        SetNextOutage(10);
        
       lamps =  GameObject.FindGameObjectsWithTag("Light");
    }

    // Update is called once per frame
    void Update()
    {
        if (!outage)
        {
            if(timeUntilNextPowerOutage > 0)
            {
                timeUntilNextPowerOutage -= Time.deltaTime;
            }
            else
            {
                Outage();
            }
        }
    }



    public void Outage()
    {
        
        SoundPlayer.PlaySFX(Resources.Load<AudioClip>("SFX/SFX_Power_Cut"));

        if (firstOutage)
        {

            firstOutage = false;
            GameObject.Find("UI Text").GetComponent<UIText>().DisplayText("The power is out! I should find a breaker.", 2f, false);

        }

        outage = true;
        foreach(GameObject l in lamps)
        {
            l.GetComponent<Lamp>().TurnOff();
        }

    }

    public bool isOutage() => outage;


    public void Fix()
    {
        outage = false;
        foreach (GameObject l in lamps)
        {
            l.GetComponent<Lamp>().TurnOn();
        }
        SetNextOutage();
    }

    public void SetNextOutage()
    {
        timeUntilNextPowerOutage = Random.Range(180f, 300f)/GameManager.Difficulter;
    }

    public void SetNextOutage(float time)
    {
        timeUntilNextPowerOutage = time;
    }

    public void Stop()
    {
        outage = true;
    }

    public void Go()
    {
        outage = false;
    }




}
