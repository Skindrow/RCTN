using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelDisplay : MonoBehaviour
{
    [SerializeField] private LevelData[] levelDatas;
    [SerializeField] private LevelUI levelUI;
    [SerializeField] private LevelData[] openedLevels;
    [SerializeField] private Transform parent;
    [SerializeField] private ResourceData keyData;
    [SerializeField] private ResourcesChanger energySpender;
    [SerializeField] private GameObject blockPanel;
    [SerializeField] private SceneSwitcher sceneSwitcher;
    [SerializeField] private int levelIndex;
    [SerializeField] private UnityEvent onEnergyLow;
    private string pref = "Level";

    private List<LevelUI> uiObjects = new List<LevelUI>();

    public static LevelData currentLevelData;

    private void Start()
    {
        DisplayLevels();
        LevelUI.OnLevelClick += OnLevelClick;
        LevelUI.OnLevelBuy += OpenLevel;
    }
    private void OnDestroy()
    {
        LevelUI.OnLevelClick -= OnLevelClick;
        LevelUI.OnLevelBuy -= OpenLevel;
    }
    private void DisplayLevels()
    {
        for (int i = 0; i < levelDatas.Length; i++)
        {
            LevelUI levelUIGO = Instantiate(levelUI, parent);
            levelUIGO.SetLevel(levelDatas[i].levelSprite, new Resource(keyData, levelDatas[i].keyCost), i, levelDatas[i].keyCost,
                levelDatas[i].levelName.GetLocalizedString());
            string pref = this.pref + i.ToString();
            if (!SaveSystem.HasKey(pref) || SaveSystem.LoadInt(pref) != 1)
            {
                levelUIGO.LockLevel();
                uiObjects.Add(levelUIGO);
                break;
            }
            uiObjects.Add(levelUIGO);
        }
    }
    //open - 1 , locked - 0
    private void OpenLevel(int index)
    {
        string pref = this.pref + index.ToString();
        SaveSystem.SaveInt(pref, 1);
        uiObjects[index].UnlockLevel();
        if (levelDatas.Length > (index + 1))
        {
            LevelUI levelUIGO = Instantiate(levelUI, parent);
            levelUIGO.SetLevel(levelDatas[index + 1].levelSprite, new Resource(keyData, levelDatas[index + 1].keyCost),
                index + 1, levelDatas[index + 1].keyCost, levelDatas[index + 1].levelName.GetLocalizedString());
            levelUIGO.LockLevel();
            uiObjects.Add(levelUIGO);
        }
    }
    private void OnLevelClick(int index)
    {
        bool isLocked = false;
        string pref = this.pref + index.ToString();
        if (!SaveSystem.HasKey(pref) || SaveSystem.LoadInt(pref) != 1)
        {
            isLocked = true;
        }
        if (isLocked)
        {
            uiObjects[index].BuyLevel();
        }
        else
        {
            LoadLevel(index);
        }
    }
    private void LoadLevel(int index)
    {
        if (energySpender.IsCanResourceSpend())
        {
            energySpender.ResourceSpend();
            currentLevelData = levelDatas[index];
            blockPanel.SetActive(true);
            StartCoroutine(FXLoadWait());
        }
        else
        {
            onEnergyLow?.Invoke();
        }
    }
    private IEnumerator FXLoadWait()
    {
        yield return new WaitForSeconds(1f);
        sceneSwitcher.LoadScene(levelIndex);
    }
}
