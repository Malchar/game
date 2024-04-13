using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyMember
{
    public JobBase JobBase { get; set; }
    public int Level { get; set; }
    public int HP { get; set; }
    public List<MoveBase> Moves { get; set; }
    public string Name { get; set; }

    public int GetMaxHP(){
        return JobBase.Brawn * 2;
    }

    public PartyMember(JobBase jobBase, int level, string name) {
        JobBase = jobBase;
        Level = level;
        Name = name;

        // initialize HP
        HP = GetMaxHP();

        // initialize moves
        SetMoves();
    }

    public int Brawn {
        get { return JobBase.Brawn + 1 * (Level - 1); }
    }

    private void SetMoves(){
         // add moves based on level
        Moves = new List<MoveBase>();
        foreach(var move in JobBase.LearnableMoves) {
            if (move.Level <= Level) {
                Moves.Add(move.MoveBase);
            }
        }
    }
}
