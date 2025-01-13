using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PrefChangerEvent : MonoBehaviour
{
    [SerializeField] private string pref;
    [SerializeField] private UnityEvent[] prefDeterminedEvents;

    private int prefInt;
    public void PrefChange(int prefChange)
    {
        PlayerPrefs.SetInt(pref, prefChange);
        PrefInitialize();
    }
    public void PreCallAndPrefChange(int prefChange)
    {
        PrefInitialize();
        PlayerPrefs.SetInt(pref, prefChange);
    }
    private void PrefInitialize()
    {
        if (PlayerPrefs.HasKey(pref))
            prefInt = PlayerPrefs.GetInt(pref);
        else
            prefInt = 0;
        if (prefDeterminedEvents.Length > prefInt)
            prefDeterminedEvents[prefInt]?.Invoke();
    }
}
