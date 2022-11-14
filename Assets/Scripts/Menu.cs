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
        if (PlayerPrefs.HasKey("Lvl"))// ���� � ������ ����� 1
            for (int i = 0; i < lvls.Length; i++)//�� ��� ���� ��������� ���������
            {
                if (i <= PlayerPrefs.GetInt("Lvl"))
                    lvls[i].interactable = true;
                else
                    lvls[i].interactable = false;//� ��� ���������/���������� ������, ���� � ��� �� ������ ������ �����
            }

        if (PlayerPrefs.HasKey("hp"))//���� ����������� ��������
            PlayerPrefs.SetInt("hp", 0);
        if (PlayerPrefs.HasKey("bg"))
            PlayerPrefs.SetInt("bg", 0);
        if (PlayerPrefs.HasKey("gg"))
            PlayerPrefs.SetInt("gg", 0);
    }

    void Update()
    {
        if (PlayerPrefs.HasKey("coins"))//� ��� � ����� ����� ������� �� ������� ����
            coinText.text = PlayerPrefs.GetInt("coins").ToString();
        else
            coinText.text = "0";
    }
    public void DelKeys()
    {
        PlayerPrefs.DeleteAll();
    }

    public void Buy_hp(int cost)//��� ������� �� ������ ������ �������� ����� ������� � � ���� ��������� ����� ���� ��������������� ������
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
