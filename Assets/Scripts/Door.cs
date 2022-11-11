using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isOpen = false;
    public Transform door;
    public Sprite mid, top; // вводит переменные 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Unlock()
    {
        isOpen = true;
        GetComponent<SpriteRenderer>().sprite = mid;
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = top;//анимация открытия двери(будет браться другой спрайт двери и заменяться на существующие)
    }

    public void Teleport(GameObject player){ // персонажа переносит с одной двери, до другой
        player.transform.position = new Vector3(door.position.x, door.position.y, player.transform.position.z);
    }
}
