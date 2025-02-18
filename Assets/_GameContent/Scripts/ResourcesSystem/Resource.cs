using System;
using UnityEngine;
[Serializable]
public class Resource
{
    public ResourceData Data;
    public int Amount;

    public Resource(ResourceData data, int amount)
    {
        this.Data = data;
        this.Amount = amount;
    }

    public void AddAmount(int value)
    {
        Amount += value;
    }

    public bool IsCanSpendAmount(int value)
    {
        if (Amount >= value)
        {
            return true;
        }
        return false;
    }
    public void SpendAmount(int value)
    {
        Amount -= value;
    }
}
