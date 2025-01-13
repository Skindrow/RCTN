using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventSwitcher : MonoBehaviour
{
    [SerializeField] private UnityEvent onTrue;
    [SerializeField] private UnityEvent onFalse;
    private bool switcher = false;

    public void CallSwitchEvent()
    {
        if (switcher)
        {
            switcher = false;
            onFalse?.Invoke();
        }
        else
        {
            switcher = true;
            onTrue?.Invoke();
        }
    }
}
