using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ResourcesStaticDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currencyText;
    [SerializeField] private Image icon;


    public void SetDisplay(ResourceData data, int current)
    {
        if (data.Icon != null)
            icon.sprite = data.Icon;
        currencyText.text = current.ToString();
    }
}
