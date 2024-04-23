using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public abstract class AbstractMove
{
    public abstract List<TargetType> TargetTypes { get; }
    public abstract string Description { get; }
    public abstract void PerformMove(BattleUnit user, List<List<BattleUnit>> listOfTargets);
}

public enum TargetType { None, Single, Party }

public class ElementVector {
    public int magnitude;
    public ElementType elementType;

    public ElementVector(int magnitude, ElementType elementType){
        this.magnitude = magnitude;
        this.elementType = elementType;
    }
}