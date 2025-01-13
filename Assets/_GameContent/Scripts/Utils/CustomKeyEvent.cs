using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomKeyEvent : MonoBehaviour
{
    [SerializeField] private KeyCode key;
    [SerializeField] private UnityEvent customEvent;

    private void Update()
    {
        if (Input.GetKeyDown(key))
        {
            customEvent?.Invoke();
        }
    }
}
