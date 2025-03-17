using UnityEngine;

public class FXSpawner : MonoBehaviour
{
    public void SpawnFX(Transform place)
    {
        GameObject fxGO = Instantiate(this.gameObject, place.position, Quaternion.identity);
        fxGO.transform.parent = null;
    }
}
