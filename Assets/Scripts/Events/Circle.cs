using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{

    private float reductionSpeed = 1f;
    private float startingScale;
    private float amplitude;
    // Start is called before the first frame update
    void Start()
    {
        reductionSpeed = reductionSpeed / GameManager.Difficulter;
        startingScale = transform.localScale.x;
        amplitude = startingScale * 1.2f;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(transform.localScale.x - reductionSpeed * Time.deltaTime, transform.localScale.y - reductionSpeed * Time.deltaTime, 0);
        }
        else
        {
            //Fail de ce qte là
            GameManager.Difficulter += 0.05f;
           Destroy(gameObject);
        }


        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dif = mousePos - new Vector2(transform.position.x,transform.position.y);

            if (dif.magnitude < amplitude*(transform.localScale.x+0.001f)/startingScale)
            {
                Clicked();
            }
        }
    }




    public void Clicked()
    {
        
        GameObject stampObject = Instantiate((GameObject)Resources.Load("Events/Stamp"));
        stampObject.transform.parent = transform.parent;
        stampObject.transform.position = transform.position;
        Destroy(gameObject);
    }
}
