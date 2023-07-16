    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioClip menuMusic;
    [SerializeField] AudioClip gameMusic;
    [SerializeField] AudioClip finishMusic;
    [SerializeField] AudioSource audioSource;

    void Awake()
    {
        audioSource.clip = menuMusic;
        audioSource.Play();
    }

    public void changeMenuMusic()
    {
        audioSource.clip = menuMusic;
        audioSource.volume = 0.7f;
        audioSource.Play();
    }
    public void changeGameMusic()
    {
        audioSource.clip = gameMusic;
        audioSource.volume = 0.7f;
        audioSource.Play();
    }
    public void changeFinishMusic()
    {
        audioSource.clip = finishMusic;
        audioSource.volume = 0.7f;
        audioSource.Play();
    }
}
