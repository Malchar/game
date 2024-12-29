using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "new Damage Bolt", menuName = "ScriptableObjects/Abilities/DamageBolt", order = 1)]
public class DamageBolt : Ability
{
    [field: SerializeField]
    public PoweredElement Damage {get; set; }
    [field: SerializeField]
    public string PlaceholderStatusEffects;

    public override void Invoke(Battler user, Battler[] targets)
    {
        // should be asynch
        // draw animation
        // apply damage
        CombatTools.DealDamage(user, Damage, targets[0]);

        // write to combat logs
        // close animations
        // return
    }
}
