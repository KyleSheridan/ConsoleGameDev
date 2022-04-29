using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct UpgradeStat
{
    public Stat stat;
    public int increaseAmount;
}

[System.Serializable]
public class Attribute
{
    public int level;
    public List<UpgradeStat> stats;

    public Attribute(List<UpgradeStat> _stats, int _startLevel)
    {
        stats = _stats;
        level = _startLevel;
    }
}
