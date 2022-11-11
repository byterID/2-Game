using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Btn : MonoBehaviour
{   
    public GameObject[] block;
    public Sprite btnDown;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MarkBox")//если на кнопку падает объект с тегом MarkBox, то
        {
            GetComponent<SpriteRenderer>().sprite = btnDown;//меняется спрайт кнопки на нажатую
            GetComponent<CircleCollider2D>().enabled = false;//выключает коллайдер на кнопке
            foreach (GameObject obj in block)
            {
                Destroy(obj);//уничтожает выбранные объекты
            }
        }
    }
}