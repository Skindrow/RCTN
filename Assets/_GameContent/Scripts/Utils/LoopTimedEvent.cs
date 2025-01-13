using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LoopTimedEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent tick;
    [SerializeField] private bool isOnStart;
    [SerializeField] private float tickTime;


    private void Start()
    {
        if (isOnStart)
        {
            StartFlow();
        }
    }
    public void StartFlow()
    {
        StartCoroutine(Flow());
    }

    private IEnumerator Flow()
    {
        while (true)
        {
            yield return new WaitForSeconds(tickTime);
            tick?.Invoke();
        }
    }
}
