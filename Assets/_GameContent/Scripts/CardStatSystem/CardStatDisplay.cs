using System.Collections.Generic;
using UnityEngine;

public class CardStatDisplay : MonoBehaviour
{
    [SerializeField] private CardStatUI cardStatUI;
    [SerializeField] private Transform parent;

    private List<CardStatUI> currentCardsUI = new List<CardStatUI>();
    private void OnEnable()
    {
        InitializeCards();
    }
    private void OnDisable()
    {
        DeleteCards();
    }
    private void InitializeCards()
    {
        print(CardStatManager.Instance);
        for (int i = 0; i < CardStatManager.Instance.Cards.Count; i++)
        {
            CardStatUI cardUI = Instantiate(cardStatUI, parent);
            Sprite icon = CardStatManager.Instance.Cards[i].Data.Icon;
            int count = CardStatManager.Instance.Cards[i].Amount;
            float percents = StatsHolder.Instance.GetValue(CardStatManager.Instance.Cards[i].Data.StatData.StatPref);
            string describe = CardStatManager.Instance.Cards[i].Data.Describe.GetLocalizedString();
            cardUI.SetStatCard(icon, count, percents, describe);
            currentCardsUI.Add(cardUI);
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
