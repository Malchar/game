using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;

[CreateAssetMenu(fileName = "new Battler Template", menuName = "ScriptableObjects/BattlerTemplate", order = 1)]
public class BattlerTemplate : ScriptableObject
{
    [field: SerializeField]
    public Stats StatsCore { get; set; }
    [field: SerializeField]
    public Stats StatsGrowth { get; set; }
    [field: SerializeField]
    public CreatureType[] CreatureTypes { get; set; }
    [field: SerializeField]
    public ElementVector ResistancesCore { get; set; }
    [field: SerializeField]
    public Animation IdleAnimation { get; set; }
    [field: SerializeField]
    public GearSet GearCore {get; set; }
    [field: SerializeField]
    public BattleRewards BattleRewards {get; set; }
    [field: SerializeField]
    public LearnableAbility[] LearnableAbilities {get; set; }
}
