using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomChanger : MonoBehaviour
{
    [SerializeField] private ResourceData data;
    [SerializeField] private int minAmount;
    [SerializeField] private int maxAmount;
    [SerializeField] private TextMeshProUGUI randomText;

    private int rnd;
    private void Start()
    {
        rnd = Random.Range(minAmount, maxAmount);
        randomText.text = rnd.ToString();
    }
    public void AddResources()
    {
        ResourcesManager.Instance.AddResource(data, rnd);
    }
}
