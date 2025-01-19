using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourcesDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currencyText;
    [SerializeField] private Resource.ResourcesType resource;

    private void Start()
    {
        ResourcesManager.Instance.OnResourceChange += UpdateDisplay;
        UpdateDisplay(resource, ResourcesManager.Instance.GetResourceAmount(resource));
    }
    private void OnDestroy()
    {
        ResourcesManager.Instance.OnResourceChange -= UpdateDisplay;
    }

    public void UpdateDisplay(Resource.ResourcesType type, int current)
    {
        if (type == resource)
        {
            currencyText.text = current.ToString();
        }
    }
}
