using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class EnergyOfflineGiver : MonoBehaviour
{
    [SerializeField] private ResourceData energyResource;
    [SerializeField] private int maxEnergy;
    [SerializeField] private GameObject timerObject;
    [SerializeField] private OfflineTimeCounter offlineTimeCounter;
    [SerializeField] private int secondsToRecharge;
    [SerializeField] private TextMeshProUGUI timerText;

    private string onlinePref = "OnlineSeconds";
    private int onlineSeconds = 0;
    private void CheckTimer()
    {
        if (!offlineTimeCounter.IsHasLastQuitTime())
            return;
        if (SaveSystem.HasKey(onlinePref))
            onlineSeconds = SaveSystem.LoadInt(onlinePref);
        long secondsOffline = offlineTimeCounter.CalculateInactiveSecondsLong() + onlineSeconds;
        int timerTicks = (int)(secondsOffline / secondsToRecharge);
        ResourcesManager.Instance.AddResourceWithLimit(energyResource, timerTicks, maxEnergy);
        if (ResourcesManager.Instance.GetResourceAmount(energyResource) < maxEnergy)
        {
            onlineSeconds = (int)(secondsOffline - timerTicks * secondsToRecharge);
            StartTimer();
        }

    }
    private Coroutine timerCoroutine;
    private void StartTimer()
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
        }
        timerCoroutine = StartCoroutine(TimerCoroutine());
    }
    private IEnumerator TimerCoroutine()
    {


        while (true)
        {
            onlineSeconds++;
            UpdateTimerText((secondsToRecharge - onlineSeconds));
            SaveSystem.SaveInt(onlinePref, onlineSeconds);
            yield return new WaitForSeconds(1);


            if (onlineSeconds > secondsToRecharge)
            {
                ResourcesManager.Instance.AddResourceWithLimit(energyResource, 1, maxEnergy);
                onlineSeconds = 0;
                if (ResourcesManager.Instance.GetResourceAmount(energyResource) < maxEnergy)
                {
                    StartTimer();
                }
            }
        }
    }
    private void OnDestroy()
    {
        
    }

    private void UpdateTimerText(int timeRemaining)
    {
        if (timerText != null)
        {
            int minutes = timeRemaining / 60;
            int seconds = timeRemaining % 60;
            timerText.text = $"{minutes:00}:{seconds:00}";
        }
    }
}
