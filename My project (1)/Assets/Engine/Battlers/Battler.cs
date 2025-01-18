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
    // these are temporary stat changes during battle
    public Stats TempStats { 
        private get { return TempStats; } 
        set {
            // check for changes
            if (TempStats.Agility - value.Agility != 0) {
                OnAgilityChanged?.Invoke(this); // Trigger the event
            }
            // set value
            TempStats = value;
        }
    }

    public Battler(ProtoBattler protoBattler, bool isEnemy)
    {
        ProtoBattler = protoBattler;
        HP = GetMaxHP();
        StatusConditions = new();
        Initiative = 0; // driven by the BattleManager's TurnQueue
        IsEnemy = isEnemy;
        TempStats = new(0, 0, 0);
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

    // the final stat values will never be negative
    public Stats GetStats() {
        Stats core = ProtoBattler.BattlerTemplate.StatsCore;
        int level = ProtoBattler.Level;
        Stats growth = ProtoBattler.BattlerTemplate.StatsGrowth;
        Stats baseStats = Stats.LevelledStats(core, level, growth);

        Stats gearStats = new(0, 0, 0); // TODO placeholder

        // floor function makes each stat non-negative
        return Stats.Add(baseStats, gearStats, TempStats).Floor();
    }

    public ElementVector GetArmor() {
        // TODO: this does not account for temp changes during battle (riposte)
        // TODO need to refactor armor and resistances in the same way as stats
        // use a tempValue which can be modified in battle, and then the getter
        // should just return the total sum including temp, base, and gear.
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

    // increment initiative and return the amount of the increase.
    internal int IncreaseInitiative()
    {
        int amount = GetStats().Agility;
        Initiative += amount;
        return amount;
    }

    public Sprite GetIcon() {
        return ProtoBattler.BattlerTemplate.Icon;
    }

    // this passes a ref to the battler, but that ref probably isn't used
    public event Action<Battler> OnAgilityChanged;

}
