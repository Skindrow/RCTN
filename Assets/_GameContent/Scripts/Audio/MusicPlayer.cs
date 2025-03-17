using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip musicClip;
    [SerializeField] private bool isOnStart;

    private void Start()
    {
        if (isOnStart)
            StartMusicPlay();
    }
    public void StartMusicPlay()
    {
        SFXController.Instance.PlayMusicClip(musicClip);
    }
    public void StopMusicPlay()
    {
        SFXController.Instance.StopMusic();
    }
}
