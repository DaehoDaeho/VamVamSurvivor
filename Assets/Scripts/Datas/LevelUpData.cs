using System;
using UnityEngine;

public enum LevelUpType
{
    MoveSpeed,
    Heal,
    ProjectileDamage,
    AttackSpeed,
    ProjectileCount
}

[Serializable]
public class LevelUpData
{
    public LevelUpType levelUpType = LevelUpType.MoveSpeed;
    public string name;
    public float value;
}
