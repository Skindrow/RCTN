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

    public delegate void ResourceChangeEvent(Resource.ResourcesType type, int current);
    public ResourceChangeEvent OnResourceChange;

    private string pref = "Resource";

    private void Start()
    {
        for (int i = 0; i < resources.Count; i++)
        {
            resources[i].Amount = LoadResource(resources[i]);
            OnResourceChange?.Invoke(resources[i].Type, resources[i].Amount);
        }
    }

    private void SaveResource(Resource res)
    {
        string modPref = pref + res.Type.ToString();
        SaveSystem.SaveInt(modPref, GetResourceAmount(res.Type));
    }
    private int LoadResource(Resource res)
    {
        string modPref = pref + res.Type.ToString();
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

    public void AddResource(Resource.ResourcesType type, int amount)
    {
        Resource resource = resources.Find(r => r.Type == type);
        if (resource != null)
        {
            resource.AddAmount(amount);
        }
        else
        {
            resources.Add(new Resource(type, amount));
        }
        SaveResource(resource);
        OnResourceChange?.Invoke(type, GetResourceAmount(type));
    }
    public void SpendResource(Resource.ResourcesType type, int amount)
    {
        Resource resource = resources.Find(r => r.Type == type);
        if (resource != null)
        {
            resource.SpendAmount(amount);
            SaveResource(resource);
        }
        else
        {
            Debug.LogError(type.ToString() + "resource not found for spend");
        }
        OnResourceChange?.Invoke(type, GetResourceAmount(type));
    }

    public bool IsCanSpendResource(Resource.ResourcesType type, int amount)
    {
        Resource resource = resources.Find(r => r.Type == type);
        if (resource != null)
        {
            return resource.IsCanSpendAmount(amount);
        }
        return false;
    }

    public int GetResourceAmount(Resource.ResourcesType type)
    {
        Resource resource = resources.Find(r => r.Type == type);
        return resource != null ? resource.Amount : 0;
    }
}

