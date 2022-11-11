using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG : MonoBehaviour
{
    float lenght, startpos;
    public GameObject cam;
    public float parallaxEffect;
    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.x;
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;//записываем длинну спрайта по оси x.
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float temp = cam.transform.position.x * (1 - parallaxEffect);//если центр фона на камере будет слишком далеко отходить от игрока, он будет подтягиваться к нему
        float dist = cam.transform.position.x * parallaxEffect;//эффективность смещения фона

        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        if (temp > startpos + lenght)
            startpos += lenght;
        else if (temp < startpos - lenght)
            startpos -= lenght;
    }
}
