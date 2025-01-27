using UnityEngine;
using UnityEngine.Localization;
[CreateAssetMenu(fileName = "new Card Data")]
public class CardStatData : ScriptableObject
{
    public Sprite Icon;
    public StatData StatData;
    public float StatAdd;
    public LocalizedString Describe;
}
