using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touches : MonoBehaviour
{
    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            if (touchPos.x > Camera.main.transform.position.x)
                transform.position = new Vector3(5f, 0f, 0f);
            else
                transform.position = new Vector3(-5f, 0f, 0f);
            transform.position = touchPos;
        }
    }
}
