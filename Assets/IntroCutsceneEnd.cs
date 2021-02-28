using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class IntroCutsceneEnd : MonoBehaviour
{

    public void ChangeScene()
    {
        GameManager.ResetValues();
        GetComponent<SceneChanger>().ChangeScene();
        Debug.Log("Change scene");
    }
    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        
    }
}
