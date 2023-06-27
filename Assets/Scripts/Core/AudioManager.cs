using System;
using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public PlayerController playerController;
    public bool isWalkingSoundPlaying;

    [Header("Walk Sound Settings")]
    [SerializeField] AudioClip[] walkingSounds;
    [SerializeField] float walkSoundInterval = 0.5f;
    [Space(5)]

    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        SingletonThisGameObject();
        isWalkingSoundPlaying = false;
    }

    private void SingletonThisGameObject()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayWalkSound()
    {
        isWalkingSoundPlaying = true;
        StartCoroutine(WalkingSound());
    }

    public void StopWalkSound()
    {
        isWalkingSoundPlaying = false;
        StopCoroutine(WalkingSound());
        audioSource.Stop();
    }

    public IEnumerator WalkingSound()
    {
        while (true)
        {
            var soundClip = walkingSounds[UnityEngine.Random.Range(0, walkingSounds.Length)];
            audioSource.PlayOneShot(soundClip);
            yield return new WaitForSeconds(walkSoundInterval);
        }
    }
}

