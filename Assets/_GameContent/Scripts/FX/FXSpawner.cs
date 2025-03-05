using UnityEngine;

public class FXSpawner : MonoBehaviour
{
    public void SpawnFX(Transform place)
    {
        Instantiate(this.gameObject, place.position, Quaternion.identity);
    }
}
