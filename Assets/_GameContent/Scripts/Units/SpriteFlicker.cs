using System.Collections;
using UnityEngine;

public class SpriteFlicker : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] sr;
    [SerializeField] private Color flickColor;
    [SerializeField] private float flickTime;
    private Color[] startColor;
    private void Start()
    {
        startColor = new Color[sr.Length];
        for (int i = 0; i < sr.Length; i++)
        {
            startColor[i] = sr[i].color;
        }
    }
    private Coroutine flickCoroutine;
    private IEnumerator FlickFlow()
    {
        for (int i = 0; i < sr.Length; i++)
        {
            sr[i].color = flickColor;
        }
        yield return new WaitForSeconds(flickTime);
        for (int i = 0; i < sr.Length; i++)
        {
            sr[i].color = startColor[i];
        }
    }
    public void Flick()
    {
        if (flickCoroutine != null)
        {
            StopCoroutine(flickCoroutine);
        }
        flickCoroutine = StartCoroutine(FlickFlow());
    }
}
