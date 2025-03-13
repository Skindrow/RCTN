using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MusicSetter : MonoBehaviour
{
    [SerializeField] private Slider slider;
    private string pref = "volumeMusicPref";

    private void Start()
    {
        StartCoroutine(SoundInitialize());
    }
    private IEnumerator SoundInitialize()
    {
        yield return null;
        slider.value = SFXController.Instance.GetMusicVolume();
    }
    public void OnValueChange(float input)
    {
        SFXController.Instance.SetMusicVolume(input);
    }
}
