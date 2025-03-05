using UnityEngine;

public class DeleteTimer : MonoBehaviour
{
    [SerializeField] private float deleteTime;
    [SerializeField] private bool isOnStart = true;
    private void Start()
    {
        if(isOnStart)
        Destroy(gameObject, deleteTime);
    }
    public void DeleteWithTimer()
    {
        Destroy(gameObject, deleteTime);
    }
}
