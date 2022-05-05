using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct InputData
{
    public float VerticalInput;
    public float HorizontalInput;
    public bool Jump;
    public bool MeleeAttack;
    public bool RangedAttack;
    public bool MagicAttack;
    public bool LevelUp;
}

public interface IInput
{
    InputData GenerateInput();
}

public abstract class BaseInput : MonoBehaviour, IInput
{
    /// <summary>
    /// Override this function to generate the input to control the player.
    /// </summary>
    public abstract InputData GenerateInput();
}
