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

    public override InputData GenerateInput()
    {
        return new InputData
        {
            VerticalInput = Input.GetAxis(verticalInputName),
            HorizontalInput = Input.GetAxis(horizontalInputName),
            Jump = Input.GetButton(jumpButtonName),
            MeleeAttack = Input.GetButton(meleeInputName),
            RangedAttack = Input.GetButton(rangedInputName),
            MagicAttack = Input.GetButton(magicInputName)
        };
    }
}
