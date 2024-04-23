using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Move", menuName = "Job/Create new move")]
public class MoveBase : ScriptableObject
{
    [TextArea]
    [SerializeField] string description;
    [SerializeField] int power;
    [SerializeField] ElementType element;

    public string Description {
        get { return description; }
    }
    public int Power {
        get { return power; }
    }
    public ElementType Element {
        get { return element; }
    }
}

public enum ElementType {
    Earth,
    Fire,
    Wind,
    Water,
    Heart
}