using UnityEngine;
using UnityEngine.Events;

public class PrefDeterminedEvent : MonoBehaviour
{
    [SerializeField] private string pref;
    [SerializeField] private int comparedPref;
    [SerializeField] private MatchCondition matchCondition;
    [SerializeField] private UnityEvent onConditionTrue;
    [SerializeField] private UnityEvent onConditionFalse;
    [SerializeField] private UnityEvent onPrefHasnt;
    [SerializeField] private bool isOnStart = false;
    public enum MatchCondition
    {
        ifBigger,
        ifSmaller,
        ifEqual,
        ifNotEqual
    }
    public void CheckPref()
    {
        int loadedPref = 0;
        if (SaveSystem.HasKey(pref))
        {
            loadedPref = SaveSystem.LoadInt(pref);
        }
        else
        {
            onPrefHasnt?.Invoke();
            return;
        }

        if (matchCondition == MatchCondition.ifBigger)
        {
            if (loadedPref > comparedPref)
            {
                onConditionTrue?.Invoke();
            }
            else
            {
                onConditionFalse?.Invoke();
            }
        }
        else if (matchCondition == MatchCondition.ifSmaller)
        {
            if (loadedPref < comparedPref)
            {
                onConditionTrue?.Invoke();
            }
            else
            {
                onConditionFalse?.Invoke();
            }
        }
        else if (matchCondition == MatchCondition.ifEqual)
        {
            if (loadedPref == comparedPref)
            {
                onConditionTrue?.Invoke();
            }
            else
            {
                onConditionFalse?.Invoke();
            }
        }
        else if (matchCondition == MatchCondition.ifNotEqual)
        {
            if (loadedPref != comparedPref)
            {
                onConditionTrue?.Invoke();
            }
            else
            {
                onConditionFalse?.Invoke();
            }
        }
    }
}
