using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaypointManager : MonoBehaviour
{
    public static WaypointManager Instance;

    public Image img;
    public Transform target;
    public TMP_Text meter;
    public Vector3 offset;

    private void Awake()
    {
        SingletonThisGameObject();
    }

    private void Update()
    {
        SetIndicatorPosition();
    }

    private void SetIndicatorPosition()
    {
        float minX = img.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;

        float minY = img.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minY;

        Vector2 pos = Camera.main.WorldToScreenPoint(target.position + offset);

        if (Vector3.Dot((target.position - transform.position), transform.forward) < 0)
        {
            if (pos.x < Screen.width / 2)
            {
                pos.x = maxX;
            }
            else
            {
                pos.x = minX;
            }
        }

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        img.transform.position = pos;
        meter.text = ((int)Vector3.Distance(target.position, Camera.main.transform.position)).ToString() + "m";
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    private void SingletonThisGameObject()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}