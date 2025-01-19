using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResourcesChanger : MonoBehaviour
{
    [SerializeField] private Resource[] resources;
    [SerializeField] private UnityEvent onChange;
    [SerializeField] private UnityEvent onNotEnough;
    public delegate void ChangeEvent(int delta);
    public ChangeEvent OnChange;
    public void SetResources(Resource resource)
    {
        resources = new Resource[] { resource };
    }
    public void SetResources(Resource[] resources)
    {
        this.resources = resources;
    }
    public bool IsCanResourceSpend()
    {
        for (int i = 0; i < resources.Length; i++)
        {
            if (!ResourcesManager.Instance.IsCanSpendResource(resources[i].Type, resources[i].Amount))
            {
                return false;
            }
        }
        return true;
    }
    public void ResuorceSpend()
    {
        for (int i = 0; i < resources.Length; i++)
        {
            ResourcesManager.Instance.SpendResource(resources[i].Type, resources[i].Amount);
            OnChange?.Invoke(-resources[i].Amount);
        }
    }
    public void ResourceAdd()
    {
        for (int i = 0; i < resources.Length; i++)
        {
            ResourcesManager.Instance.AddResource(resources[i].Type, resources[i].Amount);
            OnChange?.Invoke(resources[i].Amount);
        }
    }

}
