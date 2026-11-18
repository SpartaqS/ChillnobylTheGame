using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ReactorUptime : MonoBehaviour
{
    [Header("Current in-game time. Readonly")]
    [SerializeField] float time = 0f;
    [SerializeField] float victoryTime = 60f;

    [Header("List of events related to elapsed game time. Designers welcome")]
    public List<UptimeEvent> UptimeEvents = new List<UptimeEvent>();

    [SerializeField]
    public List<UptimeEvent> AlreadyExecutedUptimeEvents = new List<UptimeEvent>();

    public void Awake()
    {
        GameplayManager gameplayManager = FindFirstObjectByType<GameplayManager>();
        UptimeEvents.Insert(0, new UptimeEvent(victoryTime));
        UptimeEvents[0].Event.AddListener(gameplayManager.HandleGameWin);
    }
    public float GetTime()
    {
        return time;
    }

    public void AddTimeDelta(float delta)
    {
        time += delta;
        // TODO check events

        if (UptimeEvents.Count < 1)
            return;

        for (int i = UptimeEvents.Count - 1; i >= 0; i--)
        {
            UptimeEvent uptimeEvent = UptimeEvents[i];
            if (time >= uptimeEvent.Time)
            {
                uptimeEvent.Event.Invoke();
                AlreadyExecutedUptimeEvents.Add(uptimeEvent);
                UptimeEvents.RemoveAt(i);
            }
        }
    }

    public float GetVictoryTime()
    {
        return victoryTime;
    }


    [Serializable]
    public class UptimeEvent
    {
        public float Time = 0f;
        public UnityEvent Event = null;

        public UptimeEvent(float time)
        {
            Time = time;
            Event = new UnityEvent();
        }
    }
}