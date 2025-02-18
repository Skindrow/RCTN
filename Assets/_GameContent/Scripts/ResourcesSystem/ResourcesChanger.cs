using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResourcesChanger : MonoBehaviour
{
    [SerializeField] private Resource[] resources;
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
            if (!ResourcesManager.Instance.IsCanSpendResource(resources[i].Data, resources[i].Amount))
            {
                return false;
            }
        }
        return true;
    }
    public void ResourceSpend()
    {
        for (int i = 0; i < resources.Length; i++)
        {
            ResourcesManager.Instance.SpendResource(resources[i].Data, resources[i].Amount);
            OnChange?.Invoke(-resources[i].Amount);
        }
    }
    public void ResourceAdd()
    {
        for (int i = 0; i < resources.Length; i++)
        {
            ResourcesManager.Instance.AddResource(resources[i].Data, resources[i].Amount);
            OnChange?.Invoke(resources[i].Amount);
        }
    }
    public void ResourceAddWithLimit(int limit)
    {
        for (int i = 0; i < resources.Length; i++)
        {
            int currentCount = ResourcesManager.Instance.GetResourceAmount(resources[i].Data) + resources[i].Amount;
            int addedCount = resources[i].Amount;
            if (currentCount > limit)
            {
                addedCount = limit - ResourcesManager.Instance.GetResourceAmount(resources[i].Data);
            }
            ResourcesManager.Instance.AddResource(resources[i].Data, addedCount);
            OnChange?.Invoke(resources[i].Amount);
        }
    }

}
