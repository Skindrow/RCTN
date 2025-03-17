using UnityEngine;
using UnityEngine.Events;

public class PrefChangerEvent : MonoBehaviour
{
    [SerializeField] private string pref;
    [SerializeField] private UnityEvent prefDeterminedEvent;
    [SerializeField] private bool isOnStart = false;


    private int prefInt;

    private void Start()
    {
        if (isOnStart)
        {
            PrefInitialize();
        }
    }
    public void SwitchEvent()
    {
        SaveSystem.SaveInt(pref, 1);
        PrefInitialize();
    }
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
        {
            prefInt = SaveSystem.LoadInt(pref);
            if (prefInt != 0)
                prefDeterminedEvent?.Invoke();
        }
    }
}
