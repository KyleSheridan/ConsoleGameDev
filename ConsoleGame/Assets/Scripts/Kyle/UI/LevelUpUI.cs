using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class LevelUpUI : MonoBehaviour
{
    public GameObject levelUpCanvas;

    public GameObject startButton;

    public Character character;

    public LevelSystem levelSystem;

    public PlayerHealth playerHealth;

    [Header("Stat UI")]
    public TMP_Text level;
    public TMP_Text points;
    public TMP_Text vitality;
    public TMP_Text strength;
    public TMP_Text dexterity;
    public TMP_Text intelligence;

    public TMP_Text health;
    public TMP_Text defence;
    public TMP_Text magicDefence;
    public TMP_Text attackSpeed;
    public TMP_Text melee;
    public TMP_Text ranged;
    public TMP_Text magic;

    private int pointsVal;

    private void Start()
    {
        PlayerHealth.maxHealth = character.Health.Value;
    }

    void Update()
    {
        if (InputManager.Instance.input.LevelUp)
        {
            if (levelUpCanvas.activeInHierarchy)
                CloseMenu();
            else
                OpenMenu();
        }

        if(!levelUpCanvas.activeInHierarchy) { return; }

        level.text = levelSystem.level.ToString();
        points.text = pointsVal.ToString();
        vitality.text = character.Vitality.ToString();
        strength.text = character.Strength.ToString();
        dexterity.text = character.Dexterity.ToString();
        intelligence.text = character.Intelligence.ToString();

        health.text = character.Health.baseValue.ToString();
        defence.text = character.PhysicalDefence.baseValue.ToString();
        magicDefence.text = character.MagicDefence.baseValue.ToString();
        attackSpeed.text = character.AttackSpeed.baseValue.ToString();
        melee.text = character.MeleeDamage.baseValue.ToString();
        ranged.text = character.RangedDamage.baseValue.ToString();
        magic.text = character.MagicDamage.baseValue.ToString();
    }

    public void OpenMenu()
    {
        levelUpCanvas.SetActive(true);
        Time.timeScale = 0f;

        //clear selected object 
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(startButton);

        pointsVal = Mathf.Clamp(levelSystem.level - character.Level, 0, levelSystem.level);
    }

    public void CloseMenu()
    {
        levelUpCanvas.SetActive(false);
        Time.timeScale = 1f;
    }

    public void LevelVit()
    {
        if(pointsVal > 0)
        {
            character.IncreaseVitality();
            pointsVal--;
            playerHealth.FillHealth();
        }
    }

    public void LevelStr()
    {
        if (pointsVal > 0)
        {
            character.IncreaseStrength();
            pointsVal--;
            playerHealth.FillHealth();
        }
    }

    public void LevelDex()
    {
        if (pointsVal > 0)
        {
            character.IncreaseDexterity();
            pointsVal--;
            playerHealth.FillHealth();
        }
    }

    public void LevelInt()
    {
        if (pointsVal > 0)
        {
            character.IncreaseIntelligence();
            pointsVal--;
            playerHealth.FillHealth();
        }
    }
}
