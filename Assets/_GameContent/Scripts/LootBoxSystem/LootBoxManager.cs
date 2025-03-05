using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LootBoxManager : MonoBehaviour
{
    [SerializeField] private int cost;
    [SerializeField] private List<Loot> possibleLoots;
    [SerializeField] private Transform parent;
    [SerializeField] private ResourcesChanger buyer;
    [SerializeField] private UnityEvent onNotEnoughGold;
    [SerializeField] private GameObject blockPanel;
    [SerializeField] private GameObject lootButton;
    [SerializeField] private GameObject closeButton;

    private GameObject uiObject;
    private List<Loot> lootList;
    private void Start()
    {
        InitializeLoot();
    }
    private void InitializeLoot()
    {
        lootList = new List<Loot>();
        for (int i = 0; i < possibleLoots.Count; i++)
        {
            string key = possibleLoots[i].LootAdder.name;
            if (SaveSystem.HasKey(key) && SaveSystem.LoadInt(key) >= possibleLoots[i].Max)
            {
                continue;
            }
            lootList.Add(possibleLoots[i]);
        }
    }
    public void LootBoxBuy()
    {
        if (buyer.IsCanResourceSpend())
        {
            buyer.ResourceSpend();
            LootBoxGet();
        }
        else
        {
            onNotEnoughGold?.Invoke();
        }
    }
    public void LootBoxGet()
    {
        Loot selectedLoot = GetRandomLoot();
        uiObject = selectedLoot.LootAdder.InstantiateUIObject(parent);
        StartCoroutine(BlockPanelEnable());
        InitializeLoot();
    }
    private IEnumerator BlockPanelEnable()
    {
        lootButton.SetActive(false);
        closeButton.SetActive(false);
        yield return new WaitForSeconds(2f);
        blockPanel.SetActive(true);
    }
    public void DeleteUI()
    {
        Destroy(uiObject);
        blockPanel.SetActive(false);
        lootButton.SetActive(true);
        closeButton.SetActive(true);
    }
    private Loot GetRandomLoot()
    {
        float totalWeight = 0;

        // Считаем суммарный вес всех возможных лутов
        foreach (var loot in lootList)
        {
            totalWeight += loot.Weight;
        }

        // Генерируем случайное число от 0 до суммарного веса
        float randomValue = Random.Range(0, totalWeight);

        // Проходим по списку лутов и выбираем тот, который соответствует случайному числу
        foreach (var loot in lootList)
        {
            if (randomValue < loot.Weight)
            {
                string key = loot.LootAdder.name;
                SaveSystem.SaveInt(key, (SaveSystem.LoadInt(key) + 1));
                return loot;
            }
            randomValue -= loot.Weight;
        }

        return null;
    }
}
[System.Serializable]
public class Loot
{
    public AdderBehaviour LootAdder;
    public float Weight;
    public int Max;
}
