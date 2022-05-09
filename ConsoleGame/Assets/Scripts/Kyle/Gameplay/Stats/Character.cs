using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int startVitality = 1;
    public int startStrength = 1;
    public int startDexterity = 1;
    public int startIntelligence = 1;

    public int moveSpeed = 1000;

    public int Level { get; private set; }

    public int Vitality { get; private set; }
    public int Strength { get; private set; }
    public int Dexterity { get; private set; }
    public int Intelligence { get; private set; }

    public CharacterStat Health;
    public CharacterStat PhysicalDefence;
    public CharacterStat MagicDefence;
    public CharacterStat MovementSpeed;
    public CharacterStat AttackSpeed;
    public CharacterStat MeleeDamage;
    public CharacterStat RangedDamage;
    public CharacterStat MagicDamage;

    // Start is called before the first frame update
    void Awake()
    {
        Level = 1;

        Vitality = startVitality;
        Strength = startStrength;
        Dexterity = startDexterity;
        Intelligence = startIntelligence;

        MovementSpeed.baseValue = moveSpeed;

        UpdateStats();
    }

    private void UpdateStats()
    {
        Health.baseValue = 100 + (25 * Vitality);
        PhysicalDefence.baseValue = 15 + (3 * Vitality) + (2 * Strength);
        MagicDefence.baseValue = 10 + (1 * Vitality) + (3 * Intelligence);
        AttackSpeed.baseValue = 1 + (0.05f * Dexterity);
        MeleeDamage.baseValue = 50 + (7 * Strength) + (3 * Dexterity);
        RangedDamage.baseValue = 20 + (7 * Dexterity) + (2 * Strength);
        MagicDamage.baseValue = 37 + (9 * Intelligence);
    }

    public void IncreaseVitality()
    {
        Level++;
        Vitality++;

        UpdateStats();

        PlayerHealth.maxHealth = Health.Value;
    }

    public void IncreaseStrength()
    {
        Level++;
        Strength++;

        UpdateStats();
    }

    public void IncreaseDexterity()
    {
        Level++;
        Dexterity++;

        UpdateStats();
    }

    public void IncreaseIntelligence()
    {
        Level++;
        Intelligence++;

        UpdateStats();
    }
}
