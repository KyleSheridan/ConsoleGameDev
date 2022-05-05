using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleInput : BaseInput
{
    public string verticalInputName = "Vertical";
    public string horizontalInputName = "Horizontal";
    public string jumpButtonName = "C_Jump";
    public string meleeInputName = "C_Fire1";
    public string rangedInputName = "C_Fire2";
    public string magicInputName = "C_Fire3";
    public string levelUpMenuName = "C_Touch";

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
