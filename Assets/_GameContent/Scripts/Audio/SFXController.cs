using UnityEngine;

public class SFXController : MonoBehaviour
{
    [SerializeField] private AudioSource[] audioSources;
    [SerializeField] private AudioSource musicSource;
    private string volumeSFXPref = "volumeSFXPref";
    private string volumeMusicPref = "volumeMusicPref";

    public string VolumeSFXPref => volumeSFXPref;
    public string VolumeMusicPref => volumeMusicPref;
    private float defaultVolume = 0.25f;

    public float DefaultVolume => defaultVolume;

    public static SFXController Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        LoadVolume();
    }
    private int currentSourceIndex = 0;
    public void PlayClip(AudioClip clip)
    {
        if (currentSourceIndex >= audioSources.Length)
        {
            currentSourceIndex = 0;
        }
        audioSources[currentSourceIndex].clip = clip;
        audioSources[currentSourceIndex].Play();
        currentSourceIndex++;
    }
    public void SetSFXVolume(float volume)
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].volume = volume;
        }
        PlayerPrefs.SetFloat(volumeSFXPref, volume);
    }
    public float GetSFXVolume()
    {
        return audioSources[0].volume;
    }
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
        PlayerPrefs.SetFloat(volumeMusicPref, volume);
    }
    public float GetMusicVolume()
    {
        return musicSource.volume;
    }
    private void LoadVolume()
    {
        if (!PlayerPrefs.HasKey(volumeSFXPref))
        {
            SetSFXVolume(defaultVolume);
        }
        else
        {
            SetSFXVolume(PlayerPrefs.GetFloat(volumeSFXPref));
        }
        if (!PlayerPrefs.HasKey(volumeMusicPref))
        {
            SetMusicVolume(defaultVolume);
        }
        else
        {
            SetMusicVolume(PlayerPrefs.GetFloat(volumeMusicPref));
        }
    }
}
