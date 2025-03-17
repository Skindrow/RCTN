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
        SaveSystem.SaveInt(pref, prefChange);
        PrefInitialize();
    }
    public void PreCallAndPrefChange(int prefChange)
    {
        PrefInitialize();
        SaveSystem.SaveInt(pref, prefChange);
    }
    private void PrefInitialize()
    {
        if (SaveSystem.HasKey(pref))
            prefInt = SaveSystem.LoadInt(pref);
        else
            prefInt = 0;
        if (prefDeterminedEvents.Length > prefInt)
            prefDeterminedEvents[prefInt]?.Invoke();
    }
}
