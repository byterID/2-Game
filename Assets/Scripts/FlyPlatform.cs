using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyPlatform : MonoBehaviour
{
    public Transform[] points;//точки, по которым платформа движется
    public float speed = 1f;//скорость передвижения
    int i = 1;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(points[0].position.x, points[0].position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")//если игрок наступит на платформу
        {
            float posX = transform.position.x;
            float posY = transform.position.y;
            transform.position = Vector3.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);//формула скорости передвижения

            collision.gameObject.transform.position = new Vector3(collision.gameObject.transform.position.x + transform.position.x - posX, collision.gameObject.transform.position.y + transform.position.y - posY, collision.gameObject.transform.position.z);//плавное передвижение платформы между координатами точек

            if (transform.position == points[i].position)//если платформа уже достигла какой то точки, она будет двигаться к другой
            {
                if (i < points.Length - 1)
                    i++;
                else
                    i = 0;
            }
        }
    }
}
