using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public float xOffset, yOffset;
    public bool following;
    // Start is called before the first frame update
    void Start()
    {
        if (following) Follow();
        else UnFollow();


    }

    // Update is called once per frame
    void Update()
    {
        if (following)
        {
            Vector2 Pos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(xOffset, yOffset, 0);
            gameObject.transform.position = Pos;

        }
    }

    public void Follow()
    {
        following = true;
        Cursor.visible = false;
    }

    public void UnFollow()
    {
        following = false;
        Cursor.visible = true;
    }
}
