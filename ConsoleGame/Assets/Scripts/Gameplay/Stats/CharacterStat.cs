using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

[System.Serializable]
public class CharacterStat
{
    public float baseValue;

    public virtual float Value
    {
        get { 
            if (hasChanged || baseValue != lastBaseValue) {
                lastBaseValue = baseValue;
                _value = CalculateFinalValue();
                hasChanged = false;
            }
            return _value;
        }
    }

    protected bool hasChanged = true;
    protected float _value;
    protected float lastBaseValue = float.MinValue;

    protected readonly List<StatModifier> statModifiers;
    public readonly ReadOnlyCollection<StatModifier> StatModifiers;

    public CharacterStat()
    {
        statModifiers = new List<StatModifier>();
        StatModifiers = statModifiers.AsReadOnly();
    }

    public CharacterStat(float _baseValue) : this()
    {
        baseValue = _baseValue;
    }

    public virtual void AddModifier(StatModifier mod)
    {
        hasChanged = true;
        statModifiers.Add(mod);

        statModifiers.Sort(CompareModifierOrder);
    }

    protected virtual int CompareModifierOrder(StatModifier a, StatModifier b)
    {
        if (a.order < b.order)
            return -1;
        else if (a.order > b.order)
            return 1;
        else
            return 0;
    }
    
    public virtual bool RemoveModifier(StatModifier mod)
    {
        if (statModifiers.Remove(mod))
        {
            hasChanged = true;
            return true;
        }
        return false;
    }

    public virtual bool RemoveAllModifiersFromSource(object source)
    {
        bool didRemove = false;

        for (int i = statModifiers.Count - 1; i >= 0; i--)
        {
            if(statModifiers[i].source == source)
            {
                hasChanged = true;
                didRemove = true;
                statModifiers.RemoveAt(i);
            }
        }
        return didRemove;
    }

    protected virtual float CalculateFinalValue()
    {
        float finalValue = baseValue;
        float sumPercentAdd = 0;

        for (int i = 0; i < statModifiers.Count; i++)
        {
            StatModifier mod = statModifiers[i];

            if(mod.type == StatModType.Flat)
            {
                finalValue += mod.value;
            }
            else if (mod.type == StatModType.PercentAdd)
            {
                sumPercentAdd += mod.value;

                if(i + 1 >= statModifiers.Count || statModifiers[i+1].type != StatModType.PercentAdd)
                {
                    finalValue *= 1 + sumPercentAdd;
                    sumPercentAdd = 0;
                }
            }
            else if(mod.type == StatModType.PercentMulti)
            {
                finalValue *= 1 + mod.value;
            }
        }

        return (float)Math.Round(finalValue, 4);
    }
}
