using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encounter : MonoBehaviour
{
    private float timeUntilNextEncounter = 0;
    GameObject encounterEvent;
    Sanity s;
    // Start is called before the first frame update
    void Start()
    {
        encounterEvent = (GameObject)Resources.Load("Events/DemonFight");
        timeUntilNextEncounter = Random.Range(5f, 10f);
        s = GameObject.FindGameObjectWithTag("Player").GetComponent<Sanity>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timeUntilNextEncounter <= 0)
        {
            //Debug.Log(":)");
            if(!s.isInLight && !GameManager.LampeDePoche && !s.encounter)
            {
                GameObject fight = Instantiate(encounterEvent);
                fight.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y,0);
            }   
            timeUntilNextEncounter = Random.Range(5f, 10f);
        }
        else
        {
            timeUntilNextEncounter -= Time.deltaTime;
        }
    }
}
