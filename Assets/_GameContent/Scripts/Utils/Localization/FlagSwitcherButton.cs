using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using System.Collections;
public class FlagSwitcherButton : MonoBehaviour
{
    [SerializeField] private Image buttonImage;
    [SerializeField] private Sprite[] flags;
    [SerializeField] private Locale[] locales;
    private string pref = "flagLanguage";
    private int localeIndex = 0;

    private void Start()
    {
        StartCoroutine(InitializeDelay());
    }
    private IEnumerator InitializeDelay()
    {
        yield return null;
        if (!SaveSystem.HasKey(pref))
            SetLanguage(0);
        else
        {
            SetLanguage(SaveSystem.LoadInt(pref));
        }
    }
    public void ButtonSwitch()
    {
        SetLanguage(localeIndex + 1);
    }
    public void SetLanguage(int index)
    {
        localeIndex = index;
        if (localeIndex >= flags.Length)
            localeIndex = 0;

        LocalizationSettings.SelectedLocale = locales[localeIndex];
        SaveSystem.SaveInt(pref, localeIndex);
        buttonImage.sprite = flags[localeIndex];
    }
}
