using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    bool isHit = false;
    public GameObject drop;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && !isHit){ //эта часть делает так, что когда заходишь на хитбокс врага, то теряего хп
            collision.gameObject.GetComponent<Player>().RecountHp(-1);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * 8f, ForceMode2D.Impulse);//в этой строчке создается отталкивание при олучении урона

        }
    }
        public IEnumerator Death()
        {
        if(drop != null)
        {
            Instantiate(drop, transform.position, Quaternion.identity);//после смерти жука, на его месте появится тот объект, который я поставлю в инспекторе
        }
            isHit = true;
           GetComponent<Animator>().SetBool("dead", true);
           GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
           GetComponent<Collider2D>().enabled = false;
           GetComponentInChildren<Collider2D>().enabled = false;
           yield return new WaitForSeconds(2f);
           Destroy(gameObject);
        }

        public void startDeath()
        {
            StartCoroutine(Death());
        }
    
}
