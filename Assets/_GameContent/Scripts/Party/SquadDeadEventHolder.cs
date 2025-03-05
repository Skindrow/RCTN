using UnityEngine;
using UnityEngine.Events;

public class SquadDeadEventHolder : MonoBehaviour
{
    [SerializeField] private Squad squad;
    [SerializeField] private UnityEvent OnSquadDead;


    private void Start()
    {
        squad.OnSquadDeath += SquadDeadEvent;
    }
    private void OnDestroy()
    {
        squad.OnSquadDeath -= SquadDeadEvent;
    }
    private void SquadDeadEvent(Squad squad)
    {
        if (squad == this.squad)
        {
            OnSquadDead?.Invoke();
        }
    }
}
