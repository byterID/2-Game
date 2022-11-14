using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;//снова библиотека для загрузки сцен
public class UIControl : MonoBehaviour
{
    public Player player;
    public Text coinText;
    public Image[] hearts;
    public Sprite isLife, nonLife;
    public GameObject PauseScreen, WinScreen, LoseScreen;
    float timer = 0f;
    public Text timeText;
    public TimeWork timeWork;
    public float countdown;
    //свое
    public GameObject LVLChose, Main, Shop;

    void Start()//А вот это я добавил, потому что при рестарте сцены каким то боком остаются настройки, замораживающие сцену. Теперь при старте сцены, на ней сразу будет установлена нормальная скорость времени.
    {
        Time.timeScale = 1f;

        if ((int)timeWork == 2)
            timer = countdown;
    }
    public void ReloadLevel()//Это рестарт уровня для кнопочки на экране смерти
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level1");//при нажатии на кнопку, будет запускаться первый уровень
    }
    public void LoadLevel2()
    {
        SceneManager.LoadScene("Level2");
    }
    public void Update()
    {
        coinText.text = player.GetCoins().ToString();//Беру значение int монеток и конвертирую его в String чтобы вывести текстом
        //Ввожу сердца как массив и сам указываю очередность
        for (int i = 0; i < hearts.Length; i++)//если я получил урон 1 раз, значит должно убраться самое правое сердце, и так справа налево
        {
            if (player.GetHP() > i)
                hearts[i].sprite = isLife;
            else
                hearts[i].sprite = nonLife;//заменяю сердце, в случае урона на пустое
        }
        if ((int)timeWork == 1)//настройка для секундомера
        {
            timer += Time.deltaTime;//постоянно складываю к числу время которое прошло
            timeText.text = timer.ToString("F2").Replace(",", ":");//делаю вывод этого времени на экран
        }
        else if ((int)timeWork == 2)//настройка для отсчета
        {
            timer -= Time.deltaTime;//из того времени, которое я выделил, постоянно будет вычитаться настоящее время

            //timeText.text = timer.ToString("F2").Replace(",", ":");//и так же его вывод
            timeText.text = ((int)timer / 60).ToString() + ":" + ((int)timer - ((int)timer / 60) * 60).ToString("D2");
            if (timer <= 0)//и если время вышло то вызывается метод пройгрыша
                Losee();
        }
        else
            timeText.gameObject.SetActive(false);//если вот параметр None, если ничего их этих параметров не выбрано, то ничего не выводится
    }

    public void PauseOn()//Ивент включения паузы при нажатии на кнопку
    {
        Time.timeScale = 0f;//Ставлю время сцены на 0, то-есть замораживаю все
        player.enabled = false;//отключаю скрипт игрока, чтобы он не мог двигаться
        PauseScreen.SetActive(true);//Активирую экран паузы
    }
    public void PauseOff()//А тут то же самое только в точности наоборот
    {
        Time.timeScale = 1f;
        player.enabled = true;
        PauseScreen.SetActive(false);
    }

    public void Win()//Победа победа
    {
        Time.timeScale = 0f;
        player.enabled = false;
        WinScreen.SetActive(true);

        if (!PlayerPrefs.HasKey("Lvl") || PlayerPrefs.GetInt("Lvl") < SceneManager.GetActiveScene().buildIndex)//если в файле нету ключей о пройденных сценах или ключ меньше чем код активной сцены
            PlayerPrefs.SetInt("Lvl", SceneManager.GetActiveScene().buildIndex);// то сцены активируется согласно алгоритму:

        if (PlayerPrefs.HasKey("coins"))//считываю монетки при победе
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + player.GetCoins());//складываю их с теми, что уже есть
        else
            PlayerPrefs.SetInt("coins", player.GetCoins());//если монеток до этого не было, то просто начисляю их
    }
    public void Lose()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Losee()//при пройгрыше вызывается менюшка
    {
        Time.timeScale = 0f;//в ней время замораживается
        player.enabled = false;//скрипт игрока вырубается
        LoseScreen.SetActive(true);//включаю экран смерти муахахах
    }
    public void MenuLVL()//ну а это уже на кнопку в самом меню пройгрыша
    {
        SceneManager.LoadScene("Main");//это на кнопку, нажав на которую, попадаешь в главное меню
    }
    public void NextLVL()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);//Включение следующией сцены
    }
    public void ClickStart()//этот параметр включает панель с выбором уровней
    {
        LVLChose.SetActive(true);//активирует выбор
        Main.SetActive(false);//деактивирует само главное меню
    }
    public void ClickClose()//а тут наоборот
    {
        LVLChose.SetActive(false);//выключает уровни
        Main.SetActive(true);//включает меню
        Shop.SetActive(false);
    }
    public void ClickShop()
    {
        Main.SetActive(false);
        Shop.SetActive(true);
    }
}

public enum TimeWork//новый класс Для работы с типами моего секундомера
{
    None,//тут все работает как в массиве, отсчет начинается с 0, эти переменные я буду использовать далее
    Stopwatch,
    Timer
}