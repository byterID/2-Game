using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCarped : MonoBehaviour
{
    public Transform left, right;//задаю позицию точек
    private void OnTriggerStay2D(Collider2D other)//Если игрок входит в триггер
    {
        if (other.gameObject.tag == "Player")//проверка на тег игрока
        {
            RaycastHit2D leftWall = Physics2D.Raycast(left.position, Vector2.left, 0.5f);//создаю как был невидимый луч, который проверяет наличие каких-либо объектов по близости слева
            RaycastHit2D rightWall = Physics2D.Raycast(right.position, Vector2.right, 0.5f);// и справа
            if (((Input.GetAxis("Horizontal")> 0) && !rightWall.collider && (other.transform.position.x > transform.position.x)) || ((Input.GetAxis("Horizontal") < 0) && !leftWall.collider && (other.transform.position.x < transform.position.x)))//Считывание, в какую сторону движется игрок и повторяет его координаты
                transform.position = new Vector3(other.transform.position.x,transform.position.y,transform.position.z);//платформа наничает двигаться за игроком
        }
    }
}
