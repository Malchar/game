using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyMember
{
    JobBase jobBase;
    int level;
    public int HP { get; set; }
    public List<MoveBase> Moves { get; set; }

    public PartyMember(JobBase jobBase, int level) {
        this.jobBase = jobBase;
        this.level = level;

        // initialize HP
        this.HP = Brawn * 2;

        // add moves based on level
        Moves = new List<MoveBase>();
        foreach(var move in jobBase.LearnableMoves) {
            if (move.Level <= level) {
                Moves.Add(move.MoveBase);
            }
        }
    }

    public int Brawn {
        get { return jobBase.Brawn + 1 * (level - 1); }
    }
}
