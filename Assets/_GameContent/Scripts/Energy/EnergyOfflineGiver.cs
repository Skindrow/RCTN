using System.Collections;
using TMPro;
using UnityEngine;

public class EnergyOfflineGiver : MonoBehaviour
{
    [SerializeField] private ResourceData energyResource;
    [SerializeField] private int maxEnergy;
    [SerializeField] private GameObject timerObject;
    [SerializeField] private OfflineTicker ticker;

    private void Start()
    {
        InitializeEnergy();
    }
    private void OnDestroy()
    {
        ResourcesManager.Instance.OnResourceChange -= EnergyChangeEvent;
    }
    private void InitializeEnergy()
    {
        StartCoroutine(InitializeDelay());
    }
    private IEnumerator InitializeDelay()
    {
        yield return null;

        if (ResourcesManager.Instance.GetResourceAmount(energyResource) < maxEnergy)
        {
            ticker.ContinueTicking();
            timerObject.SetActive(true);
        }
        else
        {
            timerObject.SetActive(false);
        }
        if (ResourcesManager.Instance.GetResourceAmount(energyResource) >= maxEnergy)
        {
            ticker.StopTicking();
            ticker.DeleteTimer();
            timerObject.SetActive(false);
        }
        ResourcesManager.Instance.OnResourceChange += EnergyChangeEvent;
    }
    private void EnergyChangeEvent(ResourceData data, int current)
    {
        if (data != energyResource)
            return;
        if (current >= maxEnergy)
        {
            ticker.StopTicking();
            ticker.DeleteTimer();
            timerObject.SetActive(false);
        }
        else
        {
            timerObject.SetActive(true);
            ticker.ContinueTicking();
        }
    }
}
