using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    public void PlayClip(AudioClip clip)
    {
        SFXController.Instance.PlayClip(clip);
    }
}
