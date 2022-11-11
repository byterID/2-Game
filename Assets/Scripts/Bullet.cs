using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float speed = 3f;
    float TimeToDisable = 10f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SetDisabled());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    IEnumerator SetDisabled()
    {
        yield return new WaitForSeconds(TimeToDisable);
        gameObject.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        StopCoroutine(SetDisabled());
        gameObject.SetActive(false);
    }
}
