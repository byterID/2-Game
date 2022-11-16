using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Button[] lvls;
    public Text coinText;
    public Slider musicSlider, soundSlider;//¬вожу слайдеры, чтобы брать их значени€
    public Text musicText, soundText;//“ут будет отображатьс€ уровень звука

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("Lvl"))// если € прошел сцену 1
            for (int i = 0; i < lvls.Length; i++)//то дл€ мен€ откроетс€ следующа€
            {
                if (i <= PlayerPrefs.GetInt("Lvl"))
                    lvls[i].interactable = true;
                else
                    lvls[i].interactable = false;//а тут включение/выключение кнопок, если € ещЄ не прошел нужную сцену
            }

        if (PlayerPrefs.HasKey("hp"))//беру сохраненные значени€
            PlayerPrefs.SetInt("hp", 0);
        if (PlayerPrefs.HasKey("bg"))
            PlayerPrefs.SetInt("bg", 0);
        if (PlayerPrefs.HasKey("gg"))
            PlayerPrefs.SetInt("gg", 0);

        if (!PlayerPrefs.HasKey("MusicVolume"))//создаю новые ключи дл€ сохранени€
            PlayerPrefs.SetInt("MusicVolume", 3);
        if (!PlayerPrefs.HasKey("SoundVolume"))
            PlayerPrefs.SetInt("SoundVolume", 7);

        musicSlider.value = PlayerPrefs.GetInt("MusicVolume");//сохран€ю их значени€
        soundSlider.value = PlayerPrefs.GetInt("SoundVolume");
    }

    void Update()
    {
        PlayerPrefs.SetInt("MusicVolume", (int)musicSlider.value);//ќбновл€ю значение сохраненной переменной, бер€ значение со слайдера
        PlayerPrefs.SetInt("SoundVolume", (int)soundSlider.value);
        musicText.text = musicSlider.value.ToString();//ѕереписываю значение текста на новые
        soundText.text = soundSlider.value.ToString();

        if (PlayerPrefs.HasKey("coins"))//а вот и вывод самих монеток на главное меню
            coinText.text = PlayerPrefs.GetInt("coins").ToString();
        else
            coinText.text = "0";
    }
    public void DelKeys()
    {
        PlayerPrefs.DeleteAll();
    }

    public void Buy_hp(int cost)//при нажатии на нужную кнопку вычтетс€ сумма монеток и в игре персонажу будут даны соответствующие бонусы
    {
        if(PlayerPrefs.GetInt("coins") >= cost)
        {
            PlayerPrefs.SetInt("hp", PlayerPrefs.GetInt("hp") + 1);
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") - cost);
        }
    }
    public void Buy_bg(int cost)
    {
        if (PlayerPrefs.GetInt("coins") >= cost)
        {
            PlayerPrefs.SetInt("bg", PlayerPrefs.GetInt("bg") + 1);
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") - cost);
        }
    }
    public void Buy_gg(int cost)
    {
        if (PlayerPrefs.GetInt("coins") >= cost)
        {
            PlayerPrefs.SetInt("gg", PlayerPrefs.GetInt("gg") + 1);
            PlayerPrefs.SetInt("gg", PlayerPrefs.GetInt("gg") + 1);
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") - cost);
        }
    }

}
