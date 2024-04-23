using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Job", menuName = "Job/Create new job")]
public class JobBase : ScriptableObject
{
    [TextArea]
    [SerializeField] string description;
    [SerializeField] AnimatorOverrideController animatorOverrideController;

    [SerializeField] CreatureType type1;
    [SerializeField] CreatureType type2;
    [SerializeField] Sprite frontSprite;
    [SerializeField] Sprite backSprite;

    // base stats
    [SerializeField] int agility;
    [SerializeField] int brawn;
    [SerializeField] int courage;

    [SerializeField] List<LearnableMove> learnableMoves;

    [SerializeReference] IMove favoriteMove;
    public IMove FavoriteMove { get { return favoriteMove; }}

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

    public Sprite FrontSprite { get => frontSprite; set => frontSprite = value; }
    public Sprite BackSprite { get => backSprite; set => backSprite = value; }
}

[System.Serializable]
public class LearnableMove {
    [SerializeReference] IMove move;
    public IMove Move { get { return move; }}

    [SerializeField] int level;
    public int Level { get { return level; }}
}

public enum CreatureType {
    Human,
    Beast,
    Bird,
    Undead,
    Other
}

public class TypeChart {
    static float[][] chart = {// human  beast  bird  undead  other
        /* earth */new float[] {    1f,  0.5f,   2f,     1f,    1f},
        /* fire  */new float[] {    2f,    2f,   1f,     2f,    1f},
        /* wind  */new float[] {    1f,    1f, 0.5f,     1f,    1f},
        /* water */new float[] {  0.5f,    2f,   1f,     1f,    1f},
        /* heart */new float[] {  0.5f,    1f,   1f,     2f,    1f}
    };

    public static float GetEffectiveness(ElementType attacker, CreatureType defender){
        int row = (int)attacker;
        int col = (int)defender;
        return chart[row][col];
    }
}

