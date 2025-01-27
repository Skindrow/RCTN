
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEditor.TerrainTools;
using UnityEngine;

public class CardStatManager : MonoBehaviour
{
    public static CardStatManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }



    [SerializeField] private CardStatData[] allDatas;
    [HideInInspector] public List<CardStatAmount> Cards = new List<CardStatAmount>();

    private void Start()
    {
        InitializeCards();
    }

    private void InitializeCards()
    {
        for (int i = 0; i < allDatas.Length; i++)
        {
            string pref = allDatas[i].StatData.StatPref + "Card";
            if (SaveSystem.HasKey(pref))
            {
                int currentCount = SaveSystem.LoadInt(pref);
                CardStatAmount cardStatAmount = new CardStatAmount(allDatas[i], currentCount);
                Cards.Add(cardStatAmount);
            }
        }
    }

    public void AddCard(CardStatData data)
    {
        int currentCardCount = 0;
        string pref = data.StatData.StatPref + "Card";
        if (SaveSystem.HasKey((pref)))
        {
            currentCardCount = SaveSystem.LoadInt(pref);
        }
        currentCardCount++;
        SaveSystem.SaveInt(pref, currentCardCount);
        StatIncreace(data);
        bool isAdded = false;
        for (int i = 0; i < Cards.Count; i++)
        {
            if (Cards[i].Data == data)
            {
                isAdded = true;
                Cards[i].Amount++;
                break;
            }
        }
        if (!isAdded)
        {
            CardStatAmount cardStatAmount = new CardStatAmount(data, 1);
            Cards.Add(cardStatAmount);
        }
    }
    private void StatIncreace(CardStatData data)
    {
        StatsHolder.Instance.ChangeStat(data.StatData.StatPref, data.StatAdd);
    }
}
public class CardStatAmount
{
    public CardStatData Data;
    public int Amount;

    public CardStatAmount(CardStatData data, int amount)
    {
        Data = data;
        Amount = amount;
    }
}
