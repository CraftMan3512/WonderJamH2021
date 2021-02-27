using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIText : MonoBehaviour
{

    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {

        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        StartCoroutine(testCor());

    }

    IEnumerator testCor()
    {
        
        yield return new WaitForSeconds(2f);
        DisplayText("CECI EST UN TEST TEXTE!", 5f);
        
        
    }

    public void DisplayText(string displayText, float nbSecs = 2f)
    {
        
        StopAllCoroutines();
        StartCoroutine(TextDisplayCor(displayText, nbSecs));


    }

    IEnumerator TextDisplayCor(string displayText, float nbSecs = 2f)
    {

        text.text = displayText;
        
        //fade in
        while (text.color.a < 1)
        {
            
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + Time.deltaTime);
            yield return null;

        }
        
        yield return new WaitForSeconds(nbSecs);

        //fade out
        while (text.color.a > 0)
        {
            
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - Time.deltaTime);
            yield return null;

        }
        

    }
    
}
