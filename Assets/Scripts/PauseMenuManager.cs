using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu = null;

    [SerializeField] TMP_Text volumeTextValue, musicVolumeTextValue;
    [SerializeField] Volume postProcessVolume;
    [SerializeField] Slider volumeSlider, musicVolumeSlider;
    [SerializeField] Toggle motionBlurToggle;

    [SerializeField] AudioSource musicSource;


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
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
        var volume = volumeSlider.value;
        AudioListener.volume = volume;
        volumeTextValue.text = (volume*100).ToString("0.0");
        OnVolumeApply();
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
