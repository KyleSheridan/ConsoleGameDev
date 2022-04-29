using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stat", menuName = "Stat")]
public class Stat : ScriptableObject
{
    public new string name;
    public int value;

    public Stat(string _name, int _value)
    {
        name = _name;
        value = _value;
    }
}
