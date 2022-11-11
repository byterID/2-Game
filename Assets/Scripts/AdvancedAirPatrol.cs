
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedAirPatrol : MonoBehaviour
{
    public Transform[] points;
    public float speed = 2f;
    public float WaitTime = 3f;
    bool CanGo = true;
    int i = 1;
// Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = new Vector3(points[0].position.x, points[0].position.y, transform.position.z);
    }

// Update is called once per frame
    void Update()
    {
        if (CanGo)
            transform.position = Vector3.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime); //скорость
        if (transform.position == points[i].position)
        {
            if (i < points.Length - 1)
                i++;
            else
                i = 0;
                CanGo = false;
                StartCoroutine(Waiting());

        }
    }  

    IEnumerator Waiting() //остановка стрекозы
    {
        yield return new WaitForSeconds(WaitTime);
        CanGo = true;
    }
}