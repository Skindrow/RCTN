using System.Collections;
using UnityEngine;

public class TuttorInstantiate : MonoBehaviour
{
    [SerializeField] private GameObject[] tuttorSteps;
    [SerializeField] private float tick;
    private GameObject currentStep;

    private void Start()
    {
        StartCoroutine(TuttorsSwitch());
    }
    private IEnumerator TuttorsSwitch()
    {
        for (int i = 0; i < tuttorSteps.Length; i++)
        {
            if (currentStep != null)
            {
                Destroy(currentStep);
                currentStep = null;
            }
            currentStep = Instantiate(tuttorSteps[i], transform);
            yield return new WaitForSeconds(tick);
        }
        if (currentStep != null)
        {
            Destroy(currentStep);
            currentStep = null;
        }
    }
}
