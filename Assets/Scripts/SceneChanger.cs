using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneTypes
{
    
    MainMenu = 0,
    GameplayScene = 1,
    DeathScene = 2,
    IntroScene = 3,
    WinScene = 4,
    
}

public enum TransitionTypes
{
    
    CrossFade,
    DeathTransition,
    
}

public class SceneChanger : MonoBehaviour
{

    public SceneTypes nextScene = 0;
    public TransitionTypes transition;

    public void ChangeScene()
    {

        if (LevelLoader.instance != null)
        {
            
            LevelLoader.instance.LoadScene(nextScene, transition);   
            
        }

    }
    
    public static void ChangeScene(SceneTypes scene, TransitionTypes transitionType = TransitionTypes.CrossFade)
    {


        if (LevelLoader.instance != null)
        {
            
            LevelLoader.instance.LoadScene(scene, transitionType);   
            
        }

    }

}
