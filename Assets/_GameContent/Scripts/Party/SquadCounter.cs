using UnityEngine;
using TMPro;
using System.Collections;

public class SquadCounter : MonoBehaviour
{
    [SerializeField] private Squad squad;
    [SerializeField] private TextMeshProUGUI squadCounter;
    [SerializeField] private SquadReviver squadReviver;

    private void Start()
    {
        squad.OnUnitAdd += ChangeSquadEvent;
        squad.OnUnitRemove += ChangeSquadEvent;
        StartCoroutine(DisplayDelay());
    }
    private void OnDestroy()
    {
        squad.OnUnitAdd -= ChangeSquadEvent;
        squad.OnUnitRemove -= ChangeSquadEvent;
    }
    private IEnumerator DisplayDelay()
    {
        yield return null;
        ChangeSquadEvent(null);
    }
    private void ChangeSquadEvent(Unit forDelegate)
    {
        if (squad != null)
        {
            squadCounter.text = squad.UnitsCount.ToString() + "/" + squadReviver.MaxUnits.ToString();
        }
    }
}
