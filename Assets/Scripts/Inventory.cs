using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    int hp = 0, bg = 0, gg = 0;//количество подобранных предметов каждого вида
    public Sprite[] numbers;//спрайты чисел
    public Sprite is_hp, no_hp, is_bg, no_bg, is_gg, no_gg, is_key, no_key;//спрайты предметов, которые будут меняться при их подборе
    public Image hp_img, bg_img, gg_img, key_img;//картинки, спрайты которых будут меняться
    public Player player;//тот, кто будет все это дело подбирать

    private void Start()//тут производится замена спрайтов чисел на количество подобранных бонусов)
    {
        if (PlayerPrefs.GetInt("hp") > 0)
        {
            hp = PlayerPrefs.GetInt("hp");
            hp_img.sprite = is_hp;
            hp_img.transform.GetChild(0).GetComponent<Image>().sprite = numbers[hp];
        }
        if (PlayerPrefs.GetInt("bg") > 0)
        {
            bg = PlayerPrefs.GetInt("bg");
            bg_img.sprite = is_bg;
            bg_img.transform.GetChild(0).GetComponent<Image>().sprite = numbers[bg];
        }
        if (PlayerPrefs.GetInt("gg") > 0)
        {
            gg = PlayerPrefs.GetInt("gg");
            gg_img.sprite = is_gg;
            gg_img.transform.GetChild(0).GetComponent<Image>().sprite = numbers[gg];
        }
    }
    public void Add_hp()//Эти функции считают, сколько бонусов подобрано
    {
        hp++;
        hp_img.sprite = is_hp;
        hp_img.transform.GetChild(0).GetComponent<Image>().sprite = numbers[hp];
    }
    public void Add_bg()
    {
        bg++;
        bg_img.sprite = is_bg;
        bg_img.transform.GetChild(0).GetComponent<Image>().sprite = numbers[bg];
    }
    public void Add_gg()
    {
        gg++;
        gg_img.sprite = is_gg;
        gg_img.transform.GetChild(0).GetComponent<Image>().sprite = numbers[gg];
    }
    public void Add_key()//ключи считать не надо
    {
        key_img.sprite = is_key;
    }

    public void Use_hp()//а тут уже функции кнопок, которые будут убирать 1 единицу бонуса и добавлять его эффект
    {
        if (hp > 0)
        {
            hp--;
            player.RecountHp(1);
            hp_img.transform.GetChild(0).GetComponent<Image>().sprite = numbers[hp];
            if (hp == 0)
            {
                hp_img.sprite = no_hp;
            }
        }
    }
    public void Use_bg()
    {
        if (bg > 0)
        {
            bg--;
            player.BlueGem();
            bg_img.transform.GetChild(0).GetComponent<Image>().sprite = numbers[bg];
            if (bg == 0)
            {
                bg_img.sprite = no_bg;
            }
        }
    }
    public void Use_gg()
    {
        if (gg > 0)
        {
            gg--;
            player.GreenGem();
            gg_img.transform.GetChild(0).GetComponent<Image>().sprite = numbers[gg];
            if (gg == 0)
            {
                gg_img.sprite = no_gg;
            }
        }
    }

    public void RecountItems()//при вызове этой функции значения бонусов будут переписываться на существующие
    {
        PlayerPrefs.SetInt("hp", hp);
        PlayerPrefs.SetInt("bg", bg);
        PlayerPrefs.SetInt("gg", gg);
    }
}
