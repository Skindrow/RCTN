using System;
using UnityEngine;
[Serializable]
public class Resource
{
    public ResourcesType Type;
    public Sprite ResourceSprite;
    public int Amount;

    public enum ResourcesType
    {
        Coins,
        Energy,
        Keys
    }
    public Resource(ResourcesType type, int amount)
    {
        this.Type = type;
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
