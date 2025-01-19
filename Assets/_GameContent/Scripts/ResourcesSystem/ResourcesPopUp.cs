using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourcesPopUp : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private float timeBeforeDestroy;

    private void Start()
    {
        Destroy(gameObject, timeBeforeDestroy);
    }
    public void SetPopUp(int delta)
    {
        if (delta > 0)
            this.text.text = "+" + delta.ToString();
        else
            this.text.text = "-" + delta.ToString();
    }
}
