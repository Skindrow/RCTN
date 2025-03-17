using UnityEngine;

public class SFXController : MonoBehaviour
{
    [SerializeField] private AudioSource[] audioSources;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private float musicBase = 1.0f;
    [SerializeField] private float sfxBase = 1.0f;
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

    public void PlayClipWithRandomPitch(AudioClip clip)
    {
        if (currentSourceIndex >= audioSources.Length)
        {
            currentSourceIndex = 0;
        }
        audioSources[currentSourceIndex].pitch = 1f + Random.Range(-0.3f, 0.3f);
        audioSources[currentSourceIndex].clip = clip;
        audioSources[currentSourceIndex].Play();
        currentSourceIndex++;
    }
    public void PlayMusicClip(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
    public void SetSFXVolume(float volume)
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].volume = volume * sfxBase;
        }
        PlayerPrefs.SetFloat(volumeSFXPref, volume);
    }
    public float GetSFXVolume()
    {
        return audioSources[0].volume / sfxBase;
    }
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume * musicBase;
        PlayerPrefs.SetFloat(volumeMusicPref, volume);
    }
    public float GetMusicVolume()
    {
        return musicSource.volume / musicBase;
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
