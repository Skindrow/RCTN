using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXSetter : MonoBehaviour
{
    [SerializeField] private Slider slider;
    private string pref = "volumeSFXPref";

    private void Start()
    {
        StartCoroutine(SoundInitialize());
    }
    private IEnumerator SoundInitialize()
    {
        yield return null;
        slider.value = SFXController.Instance.GetSFXVolume();
    }
    public void OnValueChange(float input)
    {
        SFXController.Instance.SetSFXVolume(input);
    }
}
