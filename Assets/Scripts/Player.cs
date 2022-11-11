using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    
    Rigidbody2D rb;
    public float speed; //переменная для настройки скорости передвижения персонажа из инспектора
    public float jumpHeight;    //для настройки прыжка
    public Transform groundCheck; //нужно для проверки, на земле ли сейчас находится персонаж
    bool isGrounded;
    Animator anim;  //ввод переменной для анимации
    int curHp;
    int maxHp = 3;
    bool isHit = false;
    public UIControl main;
    public bool key = false;
    bool canTP = true;
    public bool inWater = false;
    bool isClimbing = false;
    int coins = 0;
    bool canHit = true;
    public GameObject blueGem, greenGem;
    int gemCount = 0;
    float hitTimer = 0f;
    public Image PlayerCountdown;
    float insideTimer = -1f;
    public Image insideCountDown;


    // с помощью void вводится новый контейнер 
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();    // start нужен чтобы он произвел его содержимое после запуска игры, повторно в этой же сессии он возпроизводится не будет
        curHp = maxHp;
    }

    
    void Update()   //Update воспроизводит свое содержимое один раз за кадр
    {
        if (inWater && !isClimbing)
        {
            anim.SetInteger("State", 4);
            isGrounded = true;
            if (Input.GetAxis("Horizontal") != 0)
                Flip();
        }
        else
        {
        CheckGround();
        if (Input.GetAxis("Horizontal") == 0 && ( isGrounded) && !isClimbing){
            anim.SetInteger("State",1); 
        }
        else {
            Flip(); //в одни контейнеры можно класть другие и они будут возпроизводить один из другого
            if (isGrounded && !isClimbing)
                anim.SetInteger("State", 2);
            }
        }

        if (insideTimer >= 0f)//Если таймер равен больше или равно 0, то
        {
            insideTimer += Time.deltaTime;//к таймеру прибавляется настоящее время и
            if (insideTimer >= 5f)//если отметка дошла до 5ти секунд, то умираем
            {
                insideTimer = 0f;//причем таймер обновляется и пока не добежишь до другой кнопки, страдай
                RecountHp(-1);
            }
            else
                insideCountDown.fillAmount = 1 - (insideTimer / 5f);//заполнение визуального таймера
        }
    }

    void FixedUpdate() //контейнер чтобы персонаж мог прыгать
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
            rb.AddForce(transform.up * jumpHeight, ForceMode2D.Impulse);
    }

    void Flip() //нужен для поворота персонажа, когда он идет в обратную сторону
    {
        if (Input.GetAxis("Horizontal") > 0)
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        if (Input.GetAxis("Horizontal") < 0)
            transform.localRotation = Quaternion.Euler(0, 180, 0);
    }

    void CheckGround() //проверка земли под ногами
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position,0.2f);
        isGrounded = colliders.Length > 1;
        if (!isGrounded && !isClimbing)
            anim.SetInteger("State", 3);
    }

    public void RecountHp(int deltaHp)
    {
        curHp = curHp+deltaHp;
        if (deltaHp < 0 && canHit)
        {
            curHp = curHp + deltaHp;
            StopCoroutine(OnHit());
            canHit = false;//у игрока не будет отниматься хп если он будет под эффектом неуязвимости
            isHit = true;
            StartCoroutine(OnHit());
        }
        else if (curHp > maxHp)
        {
            curHp = curHp + deltaHp;
            curHp = maxHp;//если игрок подберет +1жизнь, а у него их уже 3, то хп останется на том же уровне
        }
        if (curHp <= 0) {
            GetComponent<CapsuleCollider2D>().enabled = false;
            Invoke("Lose", 1.5f);
        }
    }

    IEnumerator OnHit()
    {
        if (isHit)
        GetComponent<SpriteRenderer>().color = new Color(1f, GetComponent<SpriteRenderer>().color.g - 0.04f, GetComponent<SpriteRenderer>().color.b - 0.04f);
        else
        GetComponent<SpriteRenderer>().color = new Color(1f, GetComponent<SpriteRenderer>().color.g + 0.04f, GetComponent<SpriteRenderer>().color.b + 0.04f);
        if (GetComponent<SpriteRenderer>().color.g == 1f)
        {
            StopCoroutine(OnHit());
            canHit = true;
        }   
        if (GetComponent<SpriteRenderer>().color.g <= 0)

            isHit = false;
        yield return new WaitForSeconds(0.02f);
        StartCoroutine(OnHit());
    }

    void Lose()
    {
        main.GetComponent<UIControl>().Losee();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Key")//подбор ключа
        {
            Destroy(collision.gameObject);
            key = true;
        }
        if(collision.gameObject.tag == "Door"){//разрешение телепортации
            if (collision.gameObject.GetComponent<Door>().isOpen && canTP)
            {
                collision.gameObject.GetComponent<Door>().Teleport(gameObject);
                canTP = false;
                StartCoroutine(TPwait());
            }
                else if(key)
                collision.gameObject.GetComponent<Door>().Unlock();
        }
        if (collision.gameObject.tag == "Coin")//Игрок подбирает объект с тегом Coin
        {
            Destroy(collision.gameObject);//этот объект уничтожается если игрок входит в коллайдер
            coins++;//на счет игрока начисляется монетка
        }
        if (collision.gameObject.tag == "Heart")//игрок входит в коллайдер сердца
        {
            Destroy(collision.gameObject);//сердце уничтожается
            RecountHp(1);//к текущему хп игрока прибавляется 1.
        }
        if (collision.gameObject.tag == "Mushroom")//Уничтожение гриба, при контакте с игроком
        {
            Destroy(collision.gameObject);
            RecountHp(-1);//убавление хп
        }
        if (collision.gameObject.tag == "BlueGem")
        {
            Destroy(collision.gameObject);
            StartCoroutine(NoHit());
        }
        if (collision.gameObject.tag == "GreenGem")
        {
            Destroy(collision.gameObject);
            StartCoroutine(SpeedBonus());
        }

        if (collision.gameObject.tag == "TimerButtonStart")//Начинается отсчет
        {
            insideTimer = 0f;
        }
        if (collision.gameObject.tag == "TimerButtonStop")//Таймер останавливается, жизни больше не теряем
        {
            insideTimer = -1f;
            insideCountDown.fillAmount = 0f;
        }


    }

    IEnumerator TPwait()
    {
        yield return new WaitForSeconds(1f);
        canTP = true;
    }

    private void OnTriggerStay2D(Collider2D collision)//игрок залезает на лестницу и у него включается параметр Kinematic 
    {
        if (collision.gameObject.tag == "Ladder")
        {
            isClimbing = true;
            rb.bodyType = RigidbodyType2D.Kinematic;
            if (Input.GetAxis("Vertical") == 0) 
            {
                anim.SetInteger("State", 5);
            }
            else
            {
                anim.SetInteger("State", 6);
                transform.Translate(Vector3.up * Input.GetAxis("Vertical") * speed * 0.5f * Time.deltaTime); //скорость подъема вверх
            }
        }
        if (collision.gameObject.tag == "Ice")//ждет пока войдем в триггер с тегом Ice
        {
            if (rb.gravityScale == 1f)//Проверка дополнительного условия, чтобы убрать баг с произвольным срабатыванием
            {
                rb.gravityScale = 7f;//увеличиваю тягу вниз
                speed *= 0.25f;//и так же уменьшаю скорость задающего движения в 4 раза
            }
        }
        if (collision.gameObject.tag == "Lava")//Лава
        {
            hitTimer += Time.deltaTime;
            if (hitTimer >= 3f)//если таймер дошел до отметки 3 секунды, то
            {
                hitTimer = 0f;
                PlayerCountdown.fillAmount = 1f;//Кружочек заполняется
                RecountHp(-1);//Отнимается здоровье
            }
            else
                PlayerCountdown.fillAmount = 1 - (hitTimer / 3f);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)//игрок слезает с лестницы и параметр возвразается Dynamic
    {
        isClimbing = false;
        if (collision.gameObject.tag == "Ladder")
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
        if (collision.gameObject.tag == "Ice")
        {
            if (rb.gravityScale == 7f)
            {
                rb.gravityScale = 1f;
                speed *= 4f;//Возвращение параметров как было
            }
        }
        if (collision.gameObject.tag == "Lava")//Выходим из лавы
        {
            hitTimer = 0;//таймер на 0
            PlayerCountdown.fillAmount = 0f;//и картинку тоже
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Trampoline")//беру из инспектора тег
            StartCoroutine(TrampolineAnim(collision.gameObject.GetComponentInParent<Animator>()));//если игрок наступает на трамплин, проигрывается анимация

        if (collision.gameObject.tag == "Quicksand")//Если игрок наступает на любой объект с тегом Quicksand, то
        {
            speed *= 0.25f;// его скорость будет снижена в 4 раза
            rb.mass *= 100f;// и его масса увеличится в 100 раз, чтобы он не мог прыгать
        }

    }

    IEnumerator TrampolineAnim(Animator an)//время отката анимации
    {
        an.SetBool("isjump", true);
        yield return new WaitForSeconds(0.5f);
        an.SetBool("isjump", false);
    }
    IEnumerator NoHit()//Настройка неуязвимости
    {
        gemCount++;
        blueGem.SetActive(true);
        CheckGems(blueGem);
        canHit = false;
        blueGem.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(4f);
        StartCoroutine(Invis(blueGem.GetComponent<SpriteRenderer>(), 0.02f));
        yield return new WaitForSeconds(1f);
        canHit = true;
        gemCount--;
        blueGem.SetActive(false);
        CheckGems(greenGem);
    }
    IEnumerator SpeedBonus()
    {
        gemCount++;
        greenGem.SetActive(true);
        CheckGems(greenGem);

        speed = speed * 2;//при подборе скорость персонажа увеличится вдвое
        greenGem.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(4f);//эффект продлится 4 секунды
        StartCoroutine(Invis(greenGem.GetComponent<SpriteRenderer>(), 0.02f));
        yield return new WaitForSeconds(1f);
        speed = speed / 2;//возвращение к истокам
        gemCount--;
        greenGem.SetActive(false);
        CheckGems(blueGem);
    }
    void CheckGems(GameObject obj)
    {
        if (gemCount == 1)
            obj.transform.localPosition = new Vector3(0f, 1.2f, obj.transform.localPosition.z);
        else if(gemCount == 2)
        {
            blueGem.transform.localPosition = new Vector3(-0.7f, 1.2f, blueGem.transform.localPosition.z);
            greenGem.transform.localPosition = new Vector3(0.7f, 1.2f, greenGem.transform.localPosition.z);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Quicksand")
        {
            speed *= 4f;//просто возвращение всех параметров как было
            rb.mass *= 0.01f;
        }


    }

    IEnumerator Invis(SpriteRenderer spr, float time)
    {
        spr.color = new Color(1f, 1f, 1f, spr.color.a - time * 2);
        yield return new WaitForSeconds(time);
        if (spr.color.a > 0)
            StartCoroutine(Invis(spr, time));
    }

    public int GetCoins()
    {
        return coins;
    }
    public int GetHP()
    {
        return curHp;
    }
}
