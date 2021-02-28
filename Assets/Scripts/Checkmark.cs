
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Checkmark : MonoBehaviour
{
    private GameObject text;
    public GameObject[] boutons = new GameObject[5];
    public float largeurEcran;
    private float textSize;


    // Start is called before the first frame update
    void Start()
    {
        largeurEcran = Screen.width;
        textSize = (9 * (largeurEcran / 500));
        
        CompletedTask(0);
    }

    // Update is called once per frame
    void Update()
    {
            
    }


    public void CompletedTask(int index)
    {

        for (int i = 0; i < boutons.Length; i++)
        {
            if (i == index)
            {
                text = new GameObject();
                text.transform.parent = boutons[i].transform;
                text.AddComponent<TextMeshProUGUI>();
                text.GetComponent<TextMeshProUGUI>().transform.position = new Vector2(text.transform.parent.position.x, text.transform.parent.position.y);
                text.GetComponent<TextMeshProUGUI>().text = "X";
                text.GetComponent<TextMeshProUGUI>().fontSize = textSize;
                text.GetComponent<TextMeshProUGUI>().fontStyle = TMPro.FontStyles.Italic | TMPro.FontStyles.Bold;
                text.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.CenterGeoAligned;
                text.GetComponent<TextMeshProUGUI>().color = new Color32(255,0,0,255);
            }
        }

    }

}

