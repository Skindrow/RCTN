using UnityEngine;
using System;
[CreateAssetMenu(fileName = "new LootData")]
public class LootData : ScriptableObject
{
    public LootType TypeOfLoot;
    public enum LootType
    {
        Resouces,
        StatCard
    }

}