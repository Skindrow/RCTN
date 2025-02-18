using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "new Resource Data")]
public class ResourceData : ScriptableObject
{
    public Sprite Icon;
    public string Pref;
    public LocalizedString Name;
}
