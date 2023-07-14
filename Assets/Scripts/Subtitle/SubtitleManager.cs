using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtitleManager : MonoBehaviour
{
    [SerializeField] GameObject subtitleCanvas;
    [SerializeField] TMPro.TextMeshProUGUI subtitleText;
    public float subtitleTime = 0.15f;

    public static SubtitleManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void StartSubtitle(string text)
    {
        subtitleCanvas.SetActive(true);
        subtitleText.text = text;
    }

    public void DisableCanvas()
    {
        subtitleCanvas.SetActive(false);
    }
}
