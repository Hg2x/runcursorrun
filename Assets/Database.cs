using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitType
{
    Player = 0,
    Enemy = 1,
    Item = 2
}

public class ObjectType
{
    public string name; // change to better implementation later
    public UnitType type;
}

public delegate void DealDamage(int damage);
public delegate void SendPlayerStats(int playerHP);
public delegate void PlayerDied();