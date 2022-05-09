using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : BaseInput
{
    public string verticalInputName = "Vertical";
    public string horizontalInputName = "Horizontal";
    public string jumpButtonName = "Jump";
    public string meleeInputName = "Fire1";
    public string rangedInputName = "Fire2";
    public string magicInputName = "Fire3";
    public string levelUpMenuName = "LevelUp";

    public override InputData GenerateInput()
    {
        return new InputData
        {
            VerticalInput = Input.GetAxisRaw(verticalInputName),
            HorizontalInput = Input.GetAxisRaw(horizontalInputName),
            Jump = Input.GetButton(jumpButtonName),
            MeleeAttack = Input.GetButtonDown(meleeInputName),
            RangedAttack = Input.GetButtonDown(rangedInputName),
            MagicAttack = Input.GetButtonDown(magicInputName),
            LevelUp = Input.GetButtonDown(levelUpMenuName)
        };
    }
}
