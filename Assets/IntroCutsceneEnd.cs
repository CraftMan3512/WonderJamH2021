using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class IntroCutsceneEnd : MonoBehaviour
{

    private bool pressOnce = false;
    public TextMeshProUGUI skipTxt;
    
    public void ChangeScene()
    {
        GameManager.ResetValues();
        GetComponent<SceneChanger>().ChangeScene();
        //Debug.Log("Change scene");
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (pressOnce)
            {

                GameManager.ResetValues();
                GetComponent<SceneChanger>().ChangeScene();

            }
            else
            {

                skipTxt.enabled = true;
                pressOnce = true;
            }

        }
        
    }
}
