using System;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Analytics;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.Searcher.Searcher;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu = null;

    [SerializeField] TMP_Text volumeTextValue, musicVolumeTextValue;
    [SerializeField] Volume postProcessVolume;
    [SerializeField] Slider volumeSlider, musicVolumeSlider;
    [SerializeField] Toggle motionBlurToggle;

    [SerializeField] AudioSource musicSource;


    public string environment = "production";

    bool paused = false;

    async void Start()
    {
        try
        {
            var options = new InitializationOptions()
                .SetEnvironmentName(environment);

            await UnityServices.InitializeAsync(options);
        }
        catch (Exception exception)
        {
            Debug.LogError("Error during Unity Services initialization: " + exception.Message);

        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                ResumeGame();
                paused = false;
            }
            else
            {
                PauseGame();
                paused = true;
            }
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    public void OnVolumeChange()
    {
        AnalyticsService.Instance.StartDataCollection();
        var volume = volumeSlider.value;
        AudioListener.volume = volume;
        volumeTextValue.text = (volume*100).ToString("0.0");
        OnVolumeApply();
        var analyticsResult = Analytics.CustomEvent("VolumeChange", new Dictionary<string, object>
        {
            { "Volume", volume }
        });

        Debug.Log($"Analytics result: {analyticsResult}");
    }

    public void OnMusicVolumeChange()
    {
        var volume = musicVolumeSlider.value;
        musicSource.volume = volume;
        musicVolumeTextValue.text = (volume * 100).ToString("0.0");
    }

    public void OnVolumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MotionBlurChange()
    {
        MotionBlur motionBlur;
        postProcessVolume.profile.TryGet(out motionBlur);
        var isActive = motionBlurToggle.isOn;
        motionBlur.active = isActive;
    }
}
