using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PartyMember
{
    [SerializeField] JobBase jobBase;
    [SerializeField] int level;
    
    public JobBase JobBase { get {
        return jobBase;
    }}
    public int Level { get {
        return level;
    }}

    public int HP { get; set; }
    public List<IMove> Moves { get; set; }
    public string Name { get; set; }

    public int GetMaxHP(){
        return JobBase.Brawn * 2;
    }

    public void Init() {

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
        Moves = new List<IMove>();
        foreach(var learnableMove in JobBase.LearnableMoves) {
            if (learnableMove.Level <= Level) {
                Moves.Add(learnableMove.Move);
            }
        }
    }

    public DamageDetails TakeDamage(IMove move, PartyMember attacker){
        // float type1 = TypeChart.GetEffectiveness(move.Element, JobBase.Type1);
        // float type2 = TypeChart.GetEffectiveness(move.Element, JobBase.Type2);
        float type1 = 1.0f;
        float type2 = 1.0f;
        
        float critical = 1f;
        if (UnityEngine.Random.value * 100f <= 6.25f) {
            critical = 2f;
        }

        var damageDetails = new DamageDetails(){
            Type = type1 * type2,
            Critical = critical,
            Fainted = false
        };

        float modifiers = UnityEngine.Random.Range(0.85f, 1f) * type1 * type2 * critical;
        float a = (2 * attacker.Level + 10) / 250f;
        float d = a;
        // float d = a * move.Dam * ((float)attacker.Brawn / Brawn) + 2;
        int damage = Mathf.FloorToInt(d * modifiers);

        HP -= damage;
        if (HP <= 0) {
            HP = 0;
            damageDetails.Fainted = true;
        }

        return damageDetails;
    }

    public IMove GetRandomMove(){
        int r = UnityEngine.Random.Range(0, Moves.Count);
        return Moves[r];
    }
}

public class DamageDetails{
    public bool Fainted {get; set;}
    public float Critical {get; set;}
    public float Type {get; set;}
}
