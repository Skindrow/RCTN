using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip[] clips;
    [SerializeField, Range(0.0f, 1.0f)] float chanceOfPlay = 1.0f;
    public void PlayClip(AudioClip clip)
    {
        SFXController.Instance.PlayClip(clip);
    }
    public void PlayClipWithRandomPitch(AudioClip clip)
    {
        SFXController.Instance.PlayClipWithRandomPitch(clip);
    }

    public void PlayClipsWithRandomPitch()
    {
        if (clips == null || clips.Length <= 0)
        {
            return;
        }
        else
        {
            float rnd = Random.Range(0.0f, 1.0f);
            if (rnd < chanceOfPlay)
            {
                int rndIndex = Random.Range(0, clips.Length);
                SFXController.Instance.PlayClipWithRandomPitch(clips[rndIndex]);
            }
        }
    }
}
