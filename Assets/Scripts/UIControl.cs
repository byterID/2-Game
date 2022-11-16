using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;//����� ���������� ��� �������� ����
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
    public GameObject inventoryPan;
    public SoundEffector soundEffector;
    public AudioSource musicSour�e, soundSour�e;//����� AudioSource, ����� ����� �������� volume � ����������� ����������� ��������
    //����
    public GameObject LVLChose, Main, Shop, Settings;

    void Start()//� ��� ��� � �������, ������ ��� ��� �������� ����� ����� �� ����� �������� ���������, �������������� �����. ������ ��� ������ �����, �� ��� ����� ����� ����������� ���������� �������� �������.
    {
        musicSour�e.volume = (float)PlayerPrefs.GetInt("MusicVolume") / 9;//���������� ��������, �������� �� 9, �� ��� ������� ���� �������� �� 0 �� 9, � AudioSource ����� ��������� ������ �� 0 �� 1
        soundSour�e.volume = (float)PlayerPrefs.GetInt("SoundVolume") / 9;
        Time.timeScale = 1f;

        if ((int)timeWork == 2)
            timer = countdown;
    }
    public void ReloadLevel()//��� ������� ������ ��� �������� �� ������ ������
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level1");//��� ������� �� ������, ����� ����������� ������ �������
    }
    public void LoadLevel2()
    {
        SceneManager.LoadScene("Level2");
    }
    public void Update()
    {
        coinText.text = player.GetCoins().ToString();//���� �������� int ������� � ����������� ��� � String ����� ������� �������
        //����� ������ ��� ������ � ��� �������� �����������
        for (int i = 0; i < hearts.Length; i++)//���� � ������� ���� 1 ���, ������ ������ �������� ����� ������ ������, � ��� ������ ������
        {
            if (player.GetHP() > i)
                hearts[i].sprite = isLife;
            else
                hearts[i].sprite = nonLife;//������� ������, � ������ ����� �� ������
        }
        if ((int)timeWork == 1)//��������� ��� �����������
        {
            timer += Time.deltaTime;//��������� ��������� � ����� ����� ������� ������
            timeText.text = timer.ToString("F2").Replace(",", ":");//����� ����� ����� ������� �� �����
        }
        else if ((int)timeWork == 2)//��������� ��� �������
        {
            timer -= Time.deltaTime;//�� ���� �������, ������� � �������, ��������� ����� ���������� ��������� �����

            //timeText.text = timer.ToString("F2").Replace(",", ":");//� ��� �� ��� �����
            timeText.text = ((int)timer / 60).ToString() + ":" + ((int)timer - ((int)timer / 60) * 60).ToString("D2");
            if (timer <= 0)//� ���� ����� ����� �� ���������� ����� ���������
                Losee();
        }
        else
            timeText.gameObject.SetActive(false);//���� ��� �������� None, ���� ������ �� ���� ���������� �� �������, �� ������ �� ���������
    }

    public void PauseOn()//����� ��������� ����� ��� ������� �� ������
    {
        Time.timeScale = 0f;//������ ����� ����� �� 0, ��-���� ����������� ���
        player.enabled = false;//�������� ������ ������, ����� �� �� ��� ���������
        PauseScreen.SetActive(true);//��������� ����� �����
    }
    public void PauseOff()//� ��� �� �� ����� ������ � �������� ��������
    {
        Time.timeScale = 1f;
        player.enabled = true;
        PauseScreen.SetActive(false);
    }

    public void Win()//������ ������
    {
        soundEffector.PlayWinSound();
        Time.timeScale = 0f;
        player.enabled = false;
        WinScreen.SetActive(true);

        if (!PlayerPrefs.HasKey("Lvl") || PlayerPrefs.GetInt("Lvl") < SceneManager.GetActiveScene().buildIndex)//���� � ����� ���� ������ � ���������� ������ ��� ���� ������ ��� ��� �������� �����
            PlayerPrefs.SetInt("Lvl", SceneManager.GetActiveScene().buildIndex);// �� ����� ������������ �������� ���������:

        if (PlayerPrefs.HasKey("coins"))//�������� ������� ��� ������
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + player.GetCoins());//��������� �� � ����, ��� ��� ����
        else
            PlayerPrefs.SetInt("coins", player.GetCoins());//���� ������� �� ����� �� ����, �� ������ �������� ��

        inventoryPan.SetActive(false);
        GetComponent<Inventory>().RecountItems();
    }
    public void Losee()//��� ��������� ���������� �������
    {
        Time.timeScale = 0f;//� ��� ����� ��������������
        player.enabled = false;//������ ������ ����������
        LoseScreen.SetActive(true);//������� ����� ������ ��������
        inventoryPan.SetActive(false);//������� ������ � ��������
        GetComponent<Inventory>().RecountItems();//������� ���������
        soundEffector.PlayLoseSound();
    }
    public void MenuLVL()//�� � ��� ��� �� ������ � ����� ���� ���������
    {
        SceneManager.LoadScene("Main");//��� �� ������, ����� �� �������, ��������� � ������� ����
    }
    public void NextLVL()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);//��������� ���������� �����
    }
    public void ClickStart()//���� �������� �������� ������ � ������� �������
    {
        LVLChose.SetActive(true);//���������� �����
        Main.SetActive(false);//������������ ���� ������� ����
    }
    public void ClickClose()//� ��� ��������
    {
        LVLChose.SetActive(false);//��������� ������
        Main.SetActive(true);//�������� ����
        Shop.SetActive(false);
        Settings.SetActive(false);
    }
    public void ClickShop()
    {
        Main.SetActive(false);
        Shop.SetActive(true);
    }
    public void CliclSettings()
    {
        Main.SetActive(false);
        Settings.SetActive(true);
    }
    public void Exit()
    {
        Application.Quit();
    }
}

public enum TimeWork//����� ����� ��� ������ � ������ ����� �����������
{
    None,//��� ��� �������� ��� � �������, ������ ���������� � 0, ��� ���������� � ���� ������������ �����
    Stopwatch,
    Timer
}