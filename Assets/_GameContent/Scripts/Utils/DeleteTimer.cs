using UnityEngine;

public class DeleteTimer : MonoBehaviour
{
    [SerializeField] private float deleteTime;

    private void Start()
    {
        Destroy(gameObject, deleteTime);
    }
}
