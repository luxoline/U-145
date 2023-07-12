using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] AudioClip[] footstepSounds;
    [SerializeField] AudioClip jumpSound;
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void PlayFootstepSound()
    {
        var clip = footstepSounds[Random.Range(0, footstepSounds.Length)];
        audioSource.PlayOneShot(clip);
    }

    void PlayJumpSound()
    {
        audioSource.PlayOneShot(jumpSound);
    }

    void PlayLandSound()
    {
        //land yerine footstep sesi kullanildi ileride degistirilebilir
        PlayFootstepSound();
    }
}
