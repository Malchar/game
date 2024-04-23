using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IMove
{
    public string Name { get; }

    // description text displayed in menus
    public string Description { get; }

    public void PerformMove(BattleUnit user, List<List<BattleUnit>> listOfTargets);
}

[System.Serializable]
public enum TargetType { None, Single, Party }

[System.Serializable]
public class ElementVector {
    [SerializeField] public int magnitude;
    [SerializeField] public ElementType elementType;
}