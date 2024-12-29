using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Battler
{
    /*
    the battler is a temporary object which only exists during battle.
    necessary fields are initialized from the held ProtoBattler, but
    grainularity is preserved in case things change during battle.
    */
    private ProtoBattler ProtoBattler {get; set; }
    public int HP {get; set; }
    public List<StatusCondition> StatusConditions {get; set; }
    public int Initiative {get; set; }
    public event Action<int> OnHealthChanged;
    public bool IsEnemy {get; private set; }

    public Battler(ProtoBattler protoBattler, bool isEnemy)
    {
        ProtoBattler = protoBattler;
        HP = GetMaxHP();
        StatusConditions = new();
        Initiative = 0; // TODO
        IsEnemy = isEnemy;
    }

    public string GetName() {
        return ProtoBattler.Name;
    }

    // battle utilities
    public Ability[] GetAbilities() {
        int level = ProtoBattler.Level;
        LearnableAbility[] learnableAbilities = ProtoBattler.BattlerTemplate.LearnableAbilities;
        List<Ability> abilities = new();
        foreach(LearnableAbility learnableAbility in learnableAbilities) {
            if (learnableAbility.LevelRequirement <= level) {
                abilities.Add(learnableAbility.Ability);
            }
        }
        return abilities.ToArray();
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

        // signal the battlerUI update
        OnHealthChanged?.Invoke(HP);
    }

    public int GetMaxHP() {
        return 25;
    }

    public bool CanBeTargeted(Ability abilitiy) {
        // TODO battler determines if they can be targeted by the incoming abilitiy.
        // example: if knocked out, cannot be targeted by attacks.
        return true;
    }
}
