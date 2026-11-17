using UnityEngine;
using TMPro;
using System;

public class TimeDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text mainText;
    [SerializeField] private Color mainTextColor = Color.white;
    [SerializeField] private bool showMaxText = false;
    [SerializeField] private TMP_Text maxText;
    [SerializeField] private Color maxTextColor = Color.white;
    private Reactor reactor;

    private void Awake()
    {
        reactor = FindFirstObjectByType<Reactor>();
        mainText.color = mainTextColor;
        if (maxText) maxText.color = maxTextColor;
        maxText.text = "05:00";
        // todo read maxText from a config place (to setup required time to "survive")
    }

    void Update()
    {
        float currentTime = reactor.GetCurrentTime();
        mainText.text = TimeSpan.FromSeconds(currentTime).ToString("mm\\:ss\\.ff");
    }
}
