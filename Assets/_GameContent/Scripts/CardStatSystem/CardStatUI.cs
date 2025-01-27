using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardStatUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private TextMeshProUGUI valueText;
    [SerializeField] private TextMeshProUGUI describeText;

    public void SetStatCard(Sprite iconSprite, int count, float value, string describe)
    {
        icon.sprite = iconSprite;
        countText.text = "x" + count.ToString();
        valueText.text = Mathf.Round(value * 100).ToString() + "%";
        describeText.text = describe;
    }
    public void SetStatCard(CardStatData cardStatData, int count)
    {
        icon.sprite = cardStatData.Icon;
        countText.text = "x" + count.ToString();
        valueText.text = Mathf.Round(cardStatData.StatAdd * 100).ToString() + "%";
        describeText.text = cardStatData.Describe.GetLocalizedString();
    }
}
