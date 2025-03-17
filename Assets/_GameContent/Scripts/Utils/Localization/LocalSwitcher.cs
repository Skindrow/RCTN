using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
//using YG;

public class LocalSwitcher : MonoBehaviour
{

    #region singleton;
    public static LocalSwitcher Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(this.gameObject);
    }

    #endregion



    [SerializeField] private Locale[] locales;
    [SerializeField] private string[] yandexLangPref;
    private string pref = "Locale";
    public int localeIndex = 999;
    private void Start()
    {
        if (SaveSystem.HasKey(pref))
        {
            int index = SaveSystem.LoadInt(pref);
            SetLanguage(index);
            localeIndex = index;
        }
        else
        {
            bool isSetted = false;
            //for (int i = 0; i < locales.Length; i++)
            //{
            //    if (yandexLangPref[i] == YG2.envir.language)
            //    {
            //        SetLanguage(i);
            //        isSetted = true;
            //        break;
            //    }
            //}
            if (!isSetted)
            {
                for (int i = 0; i < locales.Length; i++)
                {

                    if (LocalizationSettings.SelectedLocale == locales[i])
                    {
                        SetLanguage(i);
                        break;
                    }
                }

            }
        }
    }



    public void SetLanguage(int index)
    {
        LocalizationSettings.SelectedLocale = locales[index];
        SaveSystem.SaveInt(pref, index);
        localeIndex = index;

    }
}
