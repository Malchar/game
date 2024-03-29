using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Battler : MonoBehaviour
{
    // the battler is a temporary object which only exists during battle.
    // necessary fields are initialized from the held ProtoBattler, but
    // grainularity is preserved in case things change during battle.
    public ProtoBattler ProtoBattler {get; set; }
    public int HP {get; set; }
    public StatusCondition[] StatusConditions {get; set; }
    public int Initiative {get; set; }

    // graphics
    public SpriteRenderer SpriteRenderer {get; set; }
    public Animator Animator {get; set; }

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    // battle utilities
    public Move[] GetMoves() {
        int level = ProtoBattler.Level;
        LearnableMove[] learnableMoves = ProtoBattler.BattlerTemplate.LearnableMoves;
        List<Move> moves = new();
        foreach(LearnableMove learnableMove in learnableMoves) {
            if (learnableMove.LevelRequirement <= level) {
                moves.Add(learnableMove.Move);
            }
        }
        return moves.ToArray();
    }

    public Stats GetStats() {
        Stats core = ProtoBattler.BattlerTemplate.StatsCore;
        int level = ProtoBattler.Level;
        Stats growth = ProtoBattler.BattlerTemplate.StatsGrowth;
        return Stats.LevelledStats(core, level, growth);
    }

    public ElementVector GetArmor() {
        // TODO: this does not account for temp changes during battle (riposte)
        return ProtoBattler.GetGear().GetTotalArmor();
    }

    public ElementVector GetResistances() {
        // TODO: this does not account for temp changes during battle (curses)
        return ProtoBattler.GetTotalResistances();
    }

    // this applies damage to HP
    public void TakeDamage(int damage) {
        HP -= damage;
        HP = CombatTools.BoundValue(0, HP, GetMaxHP());

        if (HP == 0) {
            // TODO
        }
    }

    public int GetMaxHP() {
        return 25;
    }
}
