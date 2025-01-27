using UnityEngine;
[CreateAssetMenu(fileName = "new Card Adder", menuName = "Loot/CardAdderData")]
public class CardAdder : AdderBehaviour
{
    [SerializeField] private CardStatUI cardStatUI;
    [SerializeField] private CardStatData cardStatData;

    public override GameObject InstantiateUIObject(Transform parent)
    {
        CardStatUI cardUIGO = Instantiate(cardStatUI, parent);
        cardUIGO.SetStatCard(cardStatData, 1);
        CardStatManager.Instance.AddCard(cardStatData);
        return cardUIGO.gameObject;
    }
}
