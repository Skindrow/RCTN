using UnityEngine;

public class RandomProp : MonoBehaviour
{
    [SerializeField] private Sprite[] randomSprites;
    [SerializeField] private SpriteRenderer sr;
    private void Start()
    {
        int randomIndex = Random.Range(0, randomSprites.Length);
        sr.sprite = randomSprites[randomIndex];
    }
}
