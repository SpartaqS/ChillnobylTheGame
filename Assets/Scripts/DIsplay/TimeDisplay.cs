using UnityEngine;
using TMPro;
using System;

[RequireComponent(typeof(LocalAudioSourceManager))]
public class TimeDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text mainText;
    [SerializeField] private Color mainTextColor = Color.white;
    [SerializeField] private bool showMaxText = false;
    [SerializeField] private TMP_Text maxText;
    [SerializeField] private Color maxTextColor = Color.white;
    private Reactor reactor;
    private LocalAudioSourceManager audioSourceManager;

    [SerializeField] Animation timerAnimationClip;

    private void Awake()
    {
        reactor = FindFirstObjectByType<Reactor>();
        audioSourceManager = FindAnyObjectByType<LocalAudioSourceManager>();
        mainText.color = mainTextColor;
        if (maxText) maxText.color = maxTextColor;
        ReactorUptime reactorUptime = reactor.GetComponent<ReactorUptime>();
        // read maxText from a config place (to setup required time to "survive")
        maxText.text = GetUnifiedTimeFormat(reactorUptime.GetVictoryTime());
    }

    void Update()
    {
        float currentTime = reactor.GetCurrentTime();
        mainText.text = GetUnifiedTimeFormat(currentTime);
    }

    private string GetUnifiedTimeFormat(float timeInSeconds)
    {
        return TimeSpan.FromSeconds(timeInSeconds).ToString("mm\\:ss\\.ff");
    }

    public void CuckooEvent()
    {
        timerAnimationClip.Play();
        audioSourceManager.PlayOneShot(SoundManager.Sound.Cuckoo);
        Debug.Log("Playing Cuckoo");
    }
}
