using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResourcesManager : MonoBehaviour
{

    #region singleton
    public static ResourcesManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    #endregion

    public List<Resource> resources = new List<Resource>();

    public delegate void ResourceChangeEvent(ResourceData data, int current);
    public ResourceChangeEvent OnResourceChange;
    public ResourceChangeEvent OnResourceNotEnough;

    private string pref = "Resource";

    private void Start()
    {
        for (int i = 0; i < resources.Count; i++)
        {
            resources[i].Amount = LoadResource(resources[i]);
            OnResourceChange?.Invoke(resources[i].Data, resources[i].Amount);
        }
    }

    private void SaveResource(Resource res)
    {
        string modPref = pref + res.Data.name.ToString();
        SaveSystem.SaveInt(modPref, GetResourceAmount(res.Data));
    }
    private int LoadResource(Resource res)
    {
        string modPref = pref + res.Data.name.ToString();
        if (SaveSystem.HasKey(modPref))
        {
            return SaveSystem.LoadInt(modPref);
        }
        else
        {
            SaveResource(res);
            return res.Amount;
        }
    }

    public void AddResource(ResourceData data, int amount)
    {
        Resource resource = resources.Find(r => r.Data == data);
        if (resource != null)
        {
            resource.AddAmount(amount);
        }
        else
        {
            resources.Add(new Resource(data, amount));
        }
        SaveResource(resource);
        OnResourceChange?.Invoke(data, GetResourceAmount(data));
    }
    public void SpendResource(ResourceData data, int amount)
    {
        Resource resource = resources.Find(r => r.Data == data);
        if (resource != null)
        {
            resource.SpendAmount(amount);
            SaveResource(resource);
        }
        else
        {
            Debug.LogError(data.ToString() + "resource not found for spend");
        }
        OnResourceChange?.Invoke(data, GetResourceAmount(data));
    }

    public bool IsCanSpendResource(ResourceData data, int amount)
    {
        Resource resource = resources.Find(r => r.Data == data);
        if (resource != null)
        {
            if (resource.IsCanSpendAmount(amount))
            {
                return true;
            }
            else
            {
                OnResourceNotEnough?.Invoke(data, amount);
                return false;
            }
        }
        return false;
    }

    public int GetResourceAmount(ResourceData data)
    {
        Resource resource = resources.Find(r => r.Data == data);
        return resource != null ? resource.Amount : 0;
    }
}

