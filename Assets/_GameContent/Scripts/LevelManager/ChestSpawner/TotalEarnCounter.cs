using TMPro;
using UnityEngine;

public class TotalEarnCounter : MonoBehaviour
{
    public static TotalEarnCounter Instance;

    private void Awake()
    {
        Instance = this;
    }
    [SerializeField] private TextMeshProUGUI countText;
    private int totalEarn = 0;

    private void Start()
    {
        totalEarn = 0;
    }
    public void TotalEarnAdd(int count)
    {
        totalEarn += count;
        countText.text = totalEarn.ToString();
    }
}
