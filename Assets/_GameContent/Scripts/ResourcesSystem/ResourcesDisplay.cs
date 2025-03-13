using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class ResourcesDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currencyText;
    [SerializeField] private ResourceData data;
    [SerializeField] private Image icon;
    [SerializeField] private UnityEvent onNotEnough;

    private void Start()
    {
        if (data.Icon != null)
            icon.sprite = data.Icon;
        ResourcesManager.Instance.OnResourceChange += UpdateDisplay;
        ResourcesManager.Instance.OnResourceNotEnough += OnNotEnoughEvent;
        UpdateDisplay(data, ResourcesManager.Instance.GetResourceAmount(data));
    }
    private void OnDestroy()
    {
        ResourcesManager.Instance.OnResourceChange -= UpdateDisplay;
        ResourcesManager.Instance.OnResourceNotEnough -= OnNotEnoughEvent;
    }
    private void OnNotEnoughEvent(ResourceData data, int current)
    {
        if (this.data == data)
        {
            onNotEnough?.Invoke();
        }
    }
    public void UpdateDisplay(ResourceData data, int current)
    {
        if (this.data == data)
        {
            currencyText.text = current.ToString();
        }
    }
}
