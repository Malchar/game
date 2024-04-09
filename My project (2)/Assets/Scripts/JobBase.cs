using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "Job", menuName = "Job/Create new job")]
public class JobBase : ScriptableObject
{
    [TextArea]
    [SerializeField] string description;
    [SerializeField] AnimatorOverrideController animatorOverrideController;

    [SerializeField] CreatureType type1;
    [SerializeField] CreatureType type2;

    // base stats
    [SerializeField] int agility;
    [SerializeField] int brawn;
    [SerializeField] int courage;

    [SerializeField] List<LearnableMove> learnableMoves;

    public string Description {
        get { return description; }
    }

    public CreatureType Type1 {
        get { return type1; }
    }

    public CreatureType Type2 {
        get { return type2; }
    }

    public int Agility {
        get { return agility; }
    }

    public int Brawn {
        get { return brawn; }
    }

    public int Courage {
        get { return courage; }
    }

    public List<LearnableMove> LearnableMoves {
        get { return learnableMoves; }
    }

}

[System.Serializable]
public class LearnableMove {
    [SerializeField] MoveBase moveBase;
    [SerializeField] int level;
    public MoveBase MoveBase {
        get { return moveBase; }
    }
    public int Level {
        get { return level; }
    }
}

public enum CreatureType {
    Human,
    Beast,
    Bird,
    Undead,
    Other
}