using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization;
using System.Collections;
using UnityEngine.ResourceManagement.AsyncOperations;

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
        valueText.text = "+" + Mathf.Round(value * 100).ToString() + "%";
        describeText.text = describe;
    }

    public void SetStatCard(CardStatData cardStatData, int count)
    {
        icon.sprite = cardStatData.Icon;
        countText.text = "x" + count.ToString();
        valueText.text = Mathf.Round(cardStatData.StatAdd * 100).ToString() + "%";

        // «апускаем корутину дл€ асинхронной загрузки локализованного текста
        StartCoroutine(SetDescribeTextAsync(cardStatData.Describe));
    }

    private IEnumerator SetDescribeTextAsync(LocalizedString localizedString)
    {
        var operation = localizedString.GetLocalizedStringAsync();
        yield return operation;

        if (operation.IsDone && operation.Status == AsyncOperationStatus.Succeeded)
        {
            describeText.text = operation.Result;
        }
        else
        {
            Debug.LogError("Failed to load localized describe text.");
        }
    }
}
