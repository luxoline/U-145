using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingManager : MonoBehaviour
{
    public static PostProcessingManager Instance { get; private set; }

    [SerializeField] PostProcessVolume processVolume;
    private void Awake()
    {
        SingletonThisGameObject();
        processVolume = GetComponent<PostProcessVolume>();
    }

    public void EnablePostProcessing()
    {
        processVolume.enabled = true;
    }

    public void DisablePostProcessing()
    {
        processVolume.enabled = false;
    }

    public void DisableMotionBlur()
    {
        MotionBlur motionBlur;
        processVolume.profile.TryGetSettings(out motionBlur);
        motionBlur.enabled.value = false;
    }

    public void EnableMotionBlur()
    {
        MotionBlur motionBlur;
        processVolume.profile.TryGetSettings(out motionBlur);
        motionBlur.enabled.value = true;
    }

    private void SingletonThisGameObject()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
