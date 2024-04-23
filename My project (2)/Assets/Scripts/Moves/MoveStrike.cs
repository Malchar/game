using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Move", menuName = "Move/Create new strike")]
public class MoveStrike : ScriptableObject, IMove
{
    string IMove.Name { get { return name; }}

    // description text displayed in menus
    [TextArea]
    [SerializeField] string description;
    string IMove.Description { get { return description; }}

    // list of types of targets, such as single, whole party, etc.
    // they will be selected in order during a battle.
    [SerializeField] List<TargetType> targetTypes;
    public List<TargetType> TargetTypes { get { return targetTypes; }}

    // the amount of damage that this move deals and its element.
    // TODO: expand to a list of matrixes
    [SerializeField] ElementVector damageAmount;
    public ElementVector DamageAmount { get { return damageAmount; }}

    public void PerformMove(BattleUnit user, List<List<BattleUnit>> listOfTargets)
    {
        // display animation

        var target1 = listOfTargets[0][0];
        BattleSystem.DealDamage(user, damageAmount, target1);

        // update GUI (update during animation if feasible)
    }
}

