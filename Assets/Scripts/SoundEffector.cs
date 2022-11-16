using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffector : MonoBehaviour
{
    public AudioSource audioSource;//получаю доступ к компоненту в инспекторе
    public AudioClip jumpSound, coinSound, winSound, loseSound;//¬вожу компоненты хвуков

    public void PlayJumpSound()//контейнер дл€ вызова из другого скрипта
    {
        audioSource.PlayOneShot(jumpSound);
    }
    public void PlayCoinSound()
    {
        audioSource.PlayOneShot(coinSound);
    }
    public void PlayWinSound()
    {
        audioSource.PlayOneShot(winSound);
    }
    public void PlayLoseSound()
    {
        audioSource.PlayOneShot(loseSound);
    }
}
