using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LocaleSwitchButton : MonoBehaviour
{
    [SerializeField] private int index;
    [SerializeField] private UnityEvent onChoose;
    [SerializeField] private UnityEvent onDeselect;
    private Image buttonImage;
    public delegate void LangSwitchEvent();
    public static LangSwitchEvent OnLangSwitch;
    private void Start()
    {
        buttonImage = GetComponent<Image>();
        CheckButtonSprite();
        OnLangSwitch += CheckButtonSprite;
    }
    private void OnDestroy()
    {
        OnLangSwitch -= CheckButtonSprite;
    }
    private void CheckButtonSprite()
    {
        if (LocalSwitcher.Instance.localeIndex == index)
        {
            onChoose?.Invoke();
        }
        else
        {
            onDeselect?.Invoke();
        }
    }
    public void LangSwitch()
    {
        LocalSwitcher.Instance.SetLanguage(index);
        OnLangSwitch?.Invoke();
    }
}
