using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour
{
    public int level;
    public float currentXP;
    public float requiredXp;

    private float lerpTimer;
    private float delayTimer;
    [Header("UI")]
    public Image frontXpBar;
    public Image backXpBar;
    // Start is called before the first frame update
    void Start()
    {
        frontXpBar.fillAmount = currentXP / requiredXp;
        backXpBar.fillAmount = currentXP / requiredXp;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateXpUI();
        if (Input.GetKeyDown(KeyCode.J))
        {
            GainExperienceFlatRate(20);
        }
        if (currentXP > requiredXp)
        {
            LevelUp();
        }
    }

    public void UpdateXpUI()
    {
        float xpFraction = currentXP / requiredXp;
        float FXP = frontXpBar.fillAmount;
        if(FXP < xpFraction)
        {
            delayTimer += Time.deltaTime;
            backXpBar.fillAmount = xpFraction;
            if(delayTimer > 3)
            {
                lerpTimer += Time.deltaTime;
                float percentComplete = lerpTimer / 4;
                frontXpBar.fillAmount = Mathf.Lerp(FXP, backXpBar.fillAmount, percentComplete);
            }
        }
        
    }

    public void GainExperienceFlatRate(float xpGained)
    {
        currentXP += xpGained;
        lerpTimer = 0f;
        delayTimer = 0f;
    }

    public void LevelUp()
    {
        level++;
        frontXpBar.fillAmount = 0f;
        backXpBar.fillAmount = 0f;
        currentXP = Mathf.RoundToInt(currentXP - requiredXp);

    }
}
