using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Utils
{
    public class CustomUnityEvent : MonoBehaviour
    {
        [SerializeField] private UnityEvent customEvent;
        [SerializeField] private CallMode callMode;
        [SerializeField] private float delayTime;
        [SerializeField] private bool isDeletedAfterCall;
        private enum CallMode
        {
            onStartInstantly,
            onStartWithTimer,
            manually
        }
        private void Start()
        {
            switch (callMode)
            {
                case CallMode.onStartInstantly:
                    CallEvent();
                    break;
                case CallMode.onStartWithTimer:
                    CallEventWithTimer();
                    break;
            }
        }
        public void CallEventWithTimer()
        {
            StartCoroutine(DelayedCall());
        }
        private IEnumerator DelayedCall()
        {
            yield return new WaitForSeconds(delayTime);
            CallEvent();
        }
        public void CallEvent()
        {
            customEvent?.Invoke();
            if (isDeletedAfterCall)
                Destroy(this);
        }
    }
}
