using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Button[] lvls;
    public Text coinText;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("Lvl"))// если я прошел сцену 1
            for (int i = 0; i < lvls.Length; i++)//то для меня откроется следующая
            {
                if (i <= PlayerPrefs.GetInt("Lvl"))
                    lvls[i].interactable = true;
                else
                    lvls[i].interactable = false;//а тут включение/выключение кнопок, если я ещё не прошел нужную сцену
            }
    }

    void Update()
    {
        if (PlayerPrefs.HasKey("coins"))//а вот и вывод самих монеток на главное меню
            coinText.text = PlayerPrefs.GetInt("coins").ToString();
        else
            coinText.text = "0";
    }
    public void DelKeys()
    {
        PlayerPrefs.DeleteAll();
    }

}
