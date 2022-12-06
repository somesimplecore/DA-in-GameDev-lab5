using UnityEngine;
using UnityEngine.UI;
using TMPro;
using YG;
using System;

public class OnlineChecker : MonoBehaviour
{
    public TextMeshProUGUI statusText;
    public Image statusImage;
    public DateTime LastLoginTime;
    void Start()
    {
        statusText = GameObject.Find("StatusText").GetComponent<TextMeshProUGUI>();
        statusImage = GameObject.Find("StatusImage").GetComponent<Image>();
    }

    void Update()
    {
        if(YandexGame.SDKEnabled)
        {
            statusText.text = "Online";
            statusImage.color = Color.green;
            LastLoginTime = DateTime.Now;
        }
        else
        {

            statusText.text = "Offline";
            statusImage.color = Color.red;
            var offlineDurationTime = DateTime.Now - new DateTime(2022, 10, 23, 22, 15, 0);
            Debug.Log("Player offline " + offlineDurationTime.Days + " days, " 
                + offlineDurationTime.Hours + " hours, " + offlineDurationTime.Minutes + " minutes.");
        }
    }
}
