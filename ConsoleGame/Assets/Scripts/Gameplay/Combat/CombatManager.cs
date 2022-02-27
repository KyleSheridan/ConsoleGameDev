using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public float sequenceResetTime;

    public float bufferTime = 0.5f;

    public int maxMeleeAttacks;
    public int maxRangedAttacks;
    public int maxMagicAttacks;

    enum InputType
    {
        Invalid = 0,
        IMelee,
        IRanged,
        IMagic
    };

    List<InputType> inputBuffer = new List<InputType>();

    bool inputActive = true;

    float timeSinceLastAttack = 0;
    int currentAttack = 1;
    InputType sequenceType = InputType.Invalid;
    InputType lastFrameType = InputType.Invalid;

    List<Coroutine> BufferCoroutines = new List<Coroutine>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void UpdateCombat(InputData input)
    {
        AddInputsToBuffer(input);

        InputBuffer();

        CalculateSequence();
    }

    void NextAttackInSequence(InputType type)
    {
        int maxNumAttacks = 1;

        switch (type)
        {
            case InputType.IMelee:
                maxNumAttacks = maxMeleeAttacks;
                break;
            case InputType.IRanged:
                maxNumAttacks = maxRangedAttacks;
                break;
            case InputType.IMagic:
                maxNumAttacks = maxMagicAttacks;
                inputActive = false;
                break;
            default:
                break;
        }

        if (currentAttack >= maxNumAttacks)
        {
            currentAttack = 1;
        }
        else
        {
            currentAttack++;
        }
    }

    IEnumerator AddToBuffer(InputType type, float waitTime)
    {
        inputBuffer.Add(type);

        yield return new WaitForSeconds(waitTime);

        RemoveFromBuffer();
    }

    void AddInputsToBuffer(InputData input)
    {
        if (input.MeleeAttack)
        {
            BufferCoroutines.Add(StartCoroutine(AddToBuffer(InputType.IMelee, bufferTime)));
        }
        if (input.RangedAttack)
        {
            BufferCoroutines.Add(StartCoroutine(AddToBuffer(InputType.IRanged, bufferTime)));
        }
        if (input.MagicAttack)
        {
            BufferCoroutines.Add(StartCoroutine(AddToBuffer(InputType.IMagic, bufferTime)));
        }
    }

    private void InputBuffer()
    {
        if (inputActive && inputBuffer.Count > 0)
        {
            switch (inputBuffer[0])
            {
                case InputType.IMelee:
                    MeleeAttack();
                    StopCoroutine(BufferCoroutines[0]);
                    RemoveFromBuffer();
                    inputActive = false;
                    break;
                case InputType.IRanged:
                    RangedAttack();
                    StopCoroutine(BufferCoroutines[0]);
                    RemoveFromBuffer();
                    inputActive = false;
                    break;
                case InputType.IMagic:
                    MagicAttack();
                    StopCoroutine(BufferCoroutines[0]);
                    RemoveFromBuffer();
                    inputActive = false;
                    break;
                default:
                    break;
            }
        }
    }

    void CalculateSequence()
    {
        if(sequenceType == InputType.Invalid) { return; }
        if(sequenceType != lastFrameType)
        {
            CancelSequence();

            lastFrameType = sequenceType;

            return;
        }

        timeSinceLastAttack += Time.deltaTime;

        if (timeSinceLastAttack > sequenceResetTime && inputActive)
        {
            CancelSequence();
        }

        lastFrameType = sequenceType;
    }

    private void MeleeAttack()
    {
        sequenceType = InputType.IMelee;
        StartCoroutine(MeleeDebug());
    }

    private void RangedAttack()
    {
        sequenceType = InputType.IRanged;
        StartCoroutine(RangedDebug());
    }

    private void MagicAttack()
    {
        sequenceType = InputType.IMagic;
        StartCoroutine(MagicDebug());
    }

    private void CancelSequence()
    {
        currentAttack = 1;

        timeSinceLastAttack = 0;
    }

    void RemoveFromBuffer()
    {
        if (inputBuffer.Count > 0) 
        {
            inputBuffer.RemoveAt(0);
        }
    }

    IEnumerator MeleeDebug()
    {
        Debug.Log("Melee Atack...");

        Debug.Log($"Melee Sequence: {currentAttack} / {maxMeleeAttacks}");

        yield return new WaitForSeconds(1);

        Debug.Log("Melee Finished!");

        NextAttackInSequence(sequenceType);

        inputActive = true;
    }

    IEnumerator RangedDebug()
    {
        Debug.Log("Ranged Atack...");

        Debug.Log($"Ranged Sequence: {currentAttack} / {maxRangedAttacks}");

        yield return new WaitForSeconds(1);

        Debug.Log("Ranged Finished!");

        NextAttackInSequence(sequenceType);

        inputActive = true;
    }

    IEnumerator MagicDebug()
    {
        Debug.Log("Magic Atack...");
        
        Debug.Log($"Magic Sequence: {currentAttack} / {maxMagicAttacks}");

        yield return new WaitForSeconds(1);

        Debug.Log("Magic Finished!");

        NextAttackInSequence(sequenceType);

        inputActive = true;
    }
}


