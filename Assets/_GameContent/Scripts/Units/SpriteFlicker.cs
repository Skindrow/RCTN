using System.Collections;
using UnityEngine;

public class SpriteFlicker : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color flickColor;
    [SerializeField] private float flickTime;
    private Color startColor;
    private void Start()
    {
        startColor = sr.color;
    }
    private Coroutine flickCoroutine;
    private IEnumerator FlickFlow()
    {
        sr.color = flickColor;
        yield return new WaitForSeconds(flickTime);
        sr.color = startColor;
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
