using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using System.Collections;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CardStatDisplay : MonoBehaviour
{
    [SerializeField] private CardStatUI cardStatUI;
    [SerializeField] private Transform parent;

    private List<CardStatUI> currentCardsUI = new List<CardStatUI>();

    private void OnEnable()
    {
        StartCoroutine(InitializeCardsAsync());
    }

    private void OnDisable()
    {
        DeleteCards();
    }

    private IEnumerator InitializeCardsAsync()
    {
        print(CardStatManager.Instance);

        for (int i = 0; i < CardStatManager.Instance.Cards.Count; i++)
        {
            CardStatUI cardUI = Instantiate(cardStatUI, parent);
            Sprite icon = CardStatManager.Instance.Cards[i].Data.Icon;
            int count = CardStatManager.Instance.Cards[i].Amount;
            float percents = StatsHolder.Instance.GetValue(CardStatManager.Instance.Cards[i].Data.StatData.StatPref) -
                CardStatManager.Instance.Cards[i].Data.StatData.DefaultValue;

            // Асинхронная загрузка локализованного текста
            var describeOperation = CardStatManager.Instance.Cards[i].Data.Describe.GetLocalizedStringAsync();
            yield return describeOperation;

            if (describeOperation.IsDone && describeOperation.Status == AsyncOperationStatus.Succeeded)
            {
                string describe = describeOperation.Result;
                cardUI.SetStatCard(icon, count, percents, describe);
                currentCardsUI.Add(cardUI);
            }
            else
            {
                Debug.LogError("Failed to load localized describe text for card: " + CardStatManager.Instance.Cards[i].Data.name);
            }
        }
    }

    private void DeleteCards()
    {
        for (int i = 0; i < currentCardsUI.Count; i++)
        {
            Destroy(currentCardsUI[i].gameObject);
        }
        currentCardsUI.Clear();
    }
}