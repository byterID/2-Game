using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    int hp = 0, bg = 0, gg = 0;
    public Sprite[] numbers;
    public Sprite is_hp, no_hp, is_bg, no_bg, is_gg, no_gg, is_key, no_key;
    public Image hp_img, bg_img, gg_img, key_img;
    public Player player;

    public void Add_hp()
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
    public void Add_key()
    {
        key_img.sprite = is_key;
    }

    public void Use_hp()
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
        if (hp > 0)
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
        if (hp > 0)
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
}
