using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelCost;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Image icon;
    [SerializeField] private ResourcesChanger buyer;
    [SerializeField] private Button levelButton;
    [SerializeField] private UnityEvent onLock;
    [SerializeField] private UnityEvent onUnlock;

    private int levelIndex;
    public void SetLevel(Sprite sprite, Resource res, int index, int cost, string name)
    {
        levelText.text = name;
        icon.sprite = sprite;
        buyer.SetResources(res);
        levelIndex = index;
        levelCost.text = cost.ToString();
    }
    public void LockLevel()
    {
        //levelButton.interactable = false;
        onLock?.Invoke();
    }
    public void UnlockLevel()
    {
        //levelButton.interactable = false;
        onUnlock?.Invoke();
    }
    public delegate void LevelClickEvent(int index);
    public static LevelClickEvent OnLevelClick;
    public static LevelClickEvent OnLevelBuy;
    public void LevelButtonClick()
    {
        OnLevelClick?.Invoke(levelIndex);
    }
    public void BuyLevel()
    {
        if (buyer.IsCanResourceSpend())
        {
            buyer.ResourceSpend();
            //UnlockLevel();
            OnLevelBuy?.Invoke(levelIndex);
        }
    }
}
