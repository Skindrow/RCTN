using System.Collections;
using UnityEngine;

public class RegenerationModule : MonoBehaviour
{
    [SerializeField] private HealthBehaviour healthBehaviour;
    [SerializeField] private float timesAtSecond;
    [SerializeField] private int healAmount;
    [SerializeField] private StatData timesAtSecondData;
    private void Start()
    {
        InitializeStats();
        if (timesAtSecond >= 0.0001f)
            StartCoroutine(RegenerationFlow());
    }
    private void InitializeStats()
    {
        if (timesAtSecondData != null)
        {
            timesAtSecond += timesAtSecondData.GetStat();
        }
    }
    private IEnumerator RegenerationFlow()
    {

        while (true)
        {
            yield return new WaitForSeconds(1.0f / timesAtSecond);
            healthBehaviour.Heal(healAmount);
        }
    }
}
