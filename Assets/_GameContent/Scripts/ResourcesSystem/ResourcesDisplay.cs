using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourcesDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currencyText;
    [SerializeField] private ResourceData data;
    [SerializeField] private Image icon;

    private void Start()
    {
        if (data.Icon != null)
            icon.sprite = data.Icon;
        ResourcesManager.Instance.OnResourceChange += UpdateDisplay;
        UpdateDisplay(data, ResourcesManager.Instance.GetResourceAmount(data));
    }
    private void OnDestroy()
    {
        ResourcesManager.Instance.OnResourceChange -= UpdateDisplay;
    }

    public void UpdateDisplay(ResourceData data, int current)
    {
        if (this.data == data)
        {
            currencyText.text = current.ToString();
        }
    }
}
