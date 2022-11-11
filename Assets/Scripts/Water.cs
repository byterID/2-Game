using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    float timer = 0f;
    float timerHit = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 2f)
        {
            timer = 0;
            transform.localScale = new Vector3(-1f, 1f, 1f);//анимация воды
        } else if (timer >= 1f)
            transform.localScale = new Vector3(1f, 1f, 1f);
    }

    private void OnTriggerStay2D(Collider2D collision) //создание враждебной водички, будет кусаться если игрок в неё наступит
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<Player>().inWater = true;
            timerHit += Time.deltaTime;
            if (timerHit >= 2f) //задержка перед нанесением ударов
            {
                collision.gameObject.GetComponent<Player>().RecountHp(-1);
                timerHit = 0;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) //если игрок вышел из водички то его больше не будет бить
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<Player>().inWater = false;
            timerHit = 0;
        }
    }
}
