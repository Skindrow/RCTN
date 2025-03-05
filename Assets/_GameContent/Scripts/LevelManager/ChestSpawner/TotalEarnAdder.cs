using UnityEngine;

public class TotalEarnAdder : MonoBehaviour
{
    [SerializeField] private ResourcesChanger changer;

    private void Start()
    {
        changer.OnChange += AddTotal;
    }
    private void OnDestroy()
    {
        changer.OnChange -= AddTotal;
    }

    private void AddTotal(int count)
    {
        TotalEarnCounter.Instance.TotalEarnAdd(count);
    }
}
